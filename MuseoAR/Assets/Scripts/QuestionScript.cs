using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionScript : MonoBehaviour {

    #region Properties    
    public Sprite blobYes, blobNo, blobEmpty, answerDummy;
    public Sprite correctSprite, arrowSprite;
    public GameObject fpGO;
    public Vuforia.TrackableBehaviour tb;

    private GameObject questionTextGO;
    private int correctCounter = 0, questionCounter = 0, tracking = 0, striking = 0;
    private int answer_index;
    private Question currentQuestion;
    private GameObject A, B, C, pointsGO, blobGrid;
    private GameObject[] ABC;
    private List<Question> questionList;
    private List<int> usedQuestions; //Maintain a list of the questions already used as to avoid duplicate questions during one run

    #endregion

    #region Unity Monobehaviour

    private void init()
    {
        tracking = 1;
        // quiz related inits        
        pointsGO = GameObject.Find("TextPoints");
        A = GameObject.Find("PanelAnswerA");
        B = GameObject.Find("PanelAnswerB");
        C = GameObject.Find("PanelAnswerC");
        ABC = new GameObject[] { A, B, C };
        questionTextGO = GameObject.Find("TextQuestion");
        questionList = new List<Question>();
        usedQuestions = new List<int>();
        questionTextGO = GameObject.Find("TextQuestion");
        blobGrid = GameObject.Find("BlobGrid");
        fromJsonToList();
        // hae kysymys kysymyspankista
        getQuestion();
        // odota vastausta   
    }

    private void Update()
    {
        if (tb.CurrentStatus == Vuforia.TrackableBehaviour.Status.TRACKED)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (tracking == 0)
                init();
        }

        if (pointsGO)
            pointsGO.GetComponent<Text>().text = "Kysymys " + questionCounter + "/10";
    }

#endregion

    #region Formatting methods
    /// <summary>
    /// Restart the quiz and reset correct counters and lists.
    /// </summary>
    public void restart()
    {
        // reset breadcrumb images
        var blobchildren = blobGrid.GetComponentsInChildren<Image>();
        foreach (var child in blobchildren)
        {
            child.sprite = blobEmpty;
        }        

        // disable chalkboard
        var chalkBoard = GameObject.Find("UICanvas").transform.GetChild(1).gameObject;
        if (chalkBoard)
            chalkBoard.SetActive(false);

        // reset index counters and used question list
        correctCounter = 0;
        questionCounter = 0;
        usedQuestions.Clear();

        // get a new question
        getQuestion();
    }

    /// <summary>
    /// Call the upper level script to mark this one as done.
    /// </summary>
    public void quit()
    {
        GameControllerScript.Instance.LoadTopLevelScene();
    }
#endregion

    #region Question related methods
    /// <summary>
    /// Gets a random Question from the list of questions. Registers questions as answered and excludes those.
    /// Calls the drawQuestion with the randomized question.
    /// </summary>
    public void getQuestion()
    {
        // Reset panel values and text colors for answers
        foreach (var ans in ABC)
        {
            ans.GetComponent<Button>().interactable = true;
            var img = ans.transform.GetChild(0).GetComponent<Image>();
            img.sprite = answerDummy;
            var arrow = ans.transform.GetChild(2).gameObject;
            arrow.SetActive(false);
            var text = ans.transform.GetChild(1).GetComponent<Text>();
            text.fontStyle = FontStyle.Normal;
            text.color = Color.white;            
        }

        var go = GameObject.Find("ButtonContinue");
        if (questionCounter >= 10)
        {
            Debug.Log("Nyt ollaan ShowResults ehtolohkossa");
            showResults();
        }
        else if (questionCounter >= 1 || go)
        {
            go.SetActive(false);
        }

        // kill the print
        var prints = GameObject.FindGameObjectsWithTag("Fingerprint");
        foreach (var fp in prints)
        {
            Destroy(fp);
        }

        // Register and handle used questions
        int rng = 0;
        if (usedQuestions.Count >= 0)
        {
            rng = UnityEngine.Random.Range(0, questionList.Count);
            while (usedQuestions.Contains(rng))
            {
                rng = UnityEngine.Random.Range(0, questionList.Count);
                if (!usedQuestions.Contains(rng))
                {
                    break;
                }
            }
            usedQuestions.Add(rng);
        }

        Question valittu = questionList[rng];
        currentQuestion = valittu;
        questionCounter++;
        Debug.Log("Kysymys numero" + questionCounter);
        drawQuestion(valittu);        
    }

    /// <summary>
    /// Shows the Gameover canvas with feedback
    /// </summary>
    private void showResults()
    {
        Debug.Log("Nyt ollaan ShowResults-lohkossa");
        var header = GameObject.Find("UICanvas").transform.GetChild(1).gameObject;
        header.SetActive(true);
        string performance = "";

        if (correctCounter <= 3)
        {
            performance = "Pitää vielä parantaa, yritä uudestaan!";
        }
        else if (correctCounter <= 7)
        {
            performance = "Hienosti meni!";
        }
        else if (correctCounter <= 10)
        {
            performance = "Mahtavaa!";
        }

        header.transform.GetChild(1).GetComponent<Text>().text = string.Format("Sait oikein {0}/10\n{1}", correctCounter, performance);

    }

    /// <summary>
    /// Draws the Question on the "board"
    /// </summary>
    /// <param name="que"></param>
    private void drawQuestion(Question que)
    {
        // jonkunlainen typewriter putkitushässäkkä
        var tque = questionTextGO.GetComponent<Text>();
        tque.text = que.question;
        A.transform.GetChild(1).GetComponent<Text>().text = que.answerA;
        B.transform.GetChild(1).GetComponent<Text>().text = que.answerB;
        C.transform.GetChild(1).GetComponent<Text>().text = que.answerC;
    }
    
    /// <summary>
    /// Shows the selected answer of <answerInd>.
    /// </summary>
    /// <param name="answerInd"></param>
    public void selectAnswer(int answerInd)
    {
        answer_index = answerInd;

        // Show graphically the selected answer        
        // Make a fingerprint at pressed position
        Vector3 pos = Input.mousePosition;
        var prints = GameObject.FindGameObjectsWithTag("Fingerprint");
        foreach (var fp in prints)
        {
            Destroy(fp);
        }
        Instantiate(fpGO, pos, Quaternion.identity, ABC[answerInd].transform);
        
        for (int i = 0; i < ABC.Length; i++)
        {
            if (i != answerInd)
                ABC[i].GetComponent<Button>().interactable = false;
        }

        // Delay showing correct answer
        Invoke("setCorrectAndContinue", 1);
    }

    /// <summary>
    /// Shows the correct answer as boxed and dims the wrong answers.
    /// Updates blobs in UI. Enables continuebutton.
    /// </summary>
    private void setCorrectAndContinue()
    {
        // Image variables
        var imgA = A.transform.GetChild(0).GetComponent<Image>();
        var imgB = B.transform.GetChild(0).GetComponent<Image>();
        var imgC = C.transform.GetChild(0).GetComponent<Image>();
        Image[] images = new Image[] { imgA, imgB, imgC };
        Color tmp = imgB.color;

        //Show graphically the right answer
        for (int i = 0; i < ABC.Length; i++)
        {
            tmp.a = 1;
            if (currentQuestion.correct == i)
            {
                images[i].sprite = correctSprite;                
            }
            else
            {
                // Change alphas of noncorrect answers
                for (int j = 0; j < 70; j++)
                {
                    tmp.a -= .01f;
                    ABC[i].transform.GetChild(1).GetComponent<Text>().color = tmp;
                }                
            }
        }

        // Check if correct, update blob in UI
        if (currentQuestion.correct == answer_index)
        {
            // Answered correctly
            correctCounter++;
            // Change the blob image on breadcrumb panel
            var blob = blobGrid.transform.GetChild(questionCounter - 1);
            blob.GetComponent<Image>().sprite = blobYes;
        }
        else
        {
            // Answered poorly
            // Change the blob image on breadcrumb panel
            var blob = blobGrid.transform.GetChild(questionCounter - 1);
            blob.GetComponent<Image>().sprite = blobNo;
        }

        var continueButton = GameObject.Find("ChalkBoard").transform.GetChild(7).gameObject;
        continueButton.SetActive(true);
    }
    #endregion

    #region JSON
    /// <summary>
    /// Adds all entries from questionBank.json to questionList as Question objects.
    /// </summary>
    private void fromJsonToList()
    {
        // parsing the json into token array
        Debug.Log(Application.dataPath);
        TextAsset ladattava = Resources.Load<TextAsset>("questionBank");
        rootQuestion root = JsonUtility.FromJson<rootQuestion>(ladattava.text);

        // new object and values from token
        foreach (var q in root.questions)
        {            
            questionList.Add(q);
        }
        // printQuestionList();
    }

    /// <summary>
    /// Debug printing, prints all questions in questionList to log.
    /// </summary>
    private void printQuestionList()
    {
        int i = 0;
        foreach (var q in questionList)
        {
            Debug.Log("Question #" + i);
            Debug.Log(q.ToString());
            i++;
        }
    }

    /// <summary>
    /// Question class for storing question data.
    /// </summary>
    [Serializable]
    public class Question
    {
        public string question;
        public string answerA;
        public string answerB;
        public string answerC;
        public int correct;

        public override string ToString()
        {
            return string.Format("Q: {0}, #0: {1}, #1: {2}, #2: {3}, CORRECT: {4}",
                                 question, answerA, answerB, answerC, correct);
        }
    }

    [Serializable]
    public class rootQuestion
    {
        public Question[] questions;
    }

#endregion
}