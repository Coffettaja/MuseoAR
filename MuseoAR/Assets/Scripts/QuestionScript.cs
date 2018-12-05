using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionScript : MonoBehaviour {

    #region Properties    
    public Sprite blobYes, blobNo, blobEmpty, answerYes, answerNo;
    public Vuforia.TrackableBehaviour tb;

    private GameObject questionTextGO;
    private int correctCounter = 0, questionCounter = 0, tracking = 0;
    private Question currentQuestion;
    private GameObject A, B, C, pointsGO, blobGrid;
    private GameObject[] ABC;
    private List<Question> questionList;
    private List<int> usedQuestions; //Maintain a list of the questions already used as to avoid duplicate questions during one run

    #endregion

    #region Unity Monobehaviour

    private void init()
    {
        Debug.Log("init");
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
        //Call upper scene after exiting this scene
        GameControllerScript.Instance.LoadTopLevelScene();
        // Application.Quit(); 
    }
#endregion

    #region Question related methods
    /// <summary>
    /// Gets a random (TODO: non-asked) Question from the list of questions.
    /// Calls the drawQuestion with the randomized question.
    /// </summary>
    public void getQuestion()
    {
        var go = GameObject.Find("ButtonContinue");
        A.GetComponent<Button>().interactable = true;
        B.GetComponent<Button>().interactable = true;
        C.GetComponent<Button>().interactable = true;

        // reset panel images
        var panelA = A.transform.GetChild(0).GetComponent<Image>();
        var panelB = B.transform.GetChild(0).GetComponent<Image>();
        var panelC = C.transform.GetChild(0).GetComponent<Image>();
        panelA.sprite = answerYes;
        panelB.sprite = answerYes;
        panelC.sprite = answerYes;

        A.transform.GetChild(1).GetComponent<Text>().fontStyle = FontStyle.Normal;
        B.transform.GetChild(1).GetComponent<Text>().fontStyle = FontStyle.Normal;
        C.transform.GetChild(1).GetComponent<Text>().fontStyle = FontStyle.Normal;

        A.transform.GetChild(1).GetComponent<Text>().color = Color.white;
        B.transform.GetChild(1).GetComponent<Text>().color = Color.white;
        C.transform.GetChild(1).GetComponent<Text>().color = Color.white;

        if (questionCounter >= 1 || go)
        {
            go.SetActive(false);

        }
        if (questionCounter >= 10) 
        {
            showResults();
        }

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
        drawQuestion(valittu);        
    }

    /// <summary>
    /// Shows the Gameover canvas with feedback
    /// </summary>
    private void showResults()
    {
        var continueButton = GameObject.Find("UICanvas").transform.GetChild(1).gameObject;
        continueButton.SetActive(true);
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

        continueButton.transform.GetChild(1).GetComponent<Text>().text = string.Format("Sait oikein {0}/10\n{1}", correctCounter, performance);

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
    /// Shows the selected answer of <answerInd>, overlays wrong answers with image <answerNo>.
    /// Updates breadcrumb UI. Enables continue button.
    /// </summary>
    /// <param name="answerInd"></param>
    public void selectAnswer(int answerInd)
    {
        var panelA = A.transform.GetChild(0).GetComponent<Image>();
        var panelB = B.transform.GetChild(0).GetComponent<Image>();
        var panelC = C.transform.GetChild(0).GetComponent<Image>();

        // Set color of selected answer
        var color_selected = Color.cyan;
        if (answerInd == 0)
        {
            A.transform.GetChild(1).GetComponent<Text>().fontStyle = FontStyle.Bold;
            A.transform.GetChild(1).GetComponent<Text>().color = color_selected;
            B.GetComponent<Button>().interactable = false;
            C.GetComponent<Button>().interactable = false;
        }            
        if (answerInd == 1)
        {
            B.transform.GetChild(1).GetComponent<Text>().fontStyle = FontStyle.Bold;
            B.transform.GetChild(1).GetComponent<Text>().color = color_selected;
            A.GetComponent<Button>().interactable = false;
            C.GetComponent<Button>().interactable = false;
        }
            
        if (answerInd == 2)
        {
            C.transform.GetChild(1).GetComponent<Text>().fontStyle = FontStyle.Bold;
            C.transform.GetChild(1).GetComponent<Text>().color = color_selected;
            A.GetComponent<Button>().interactable = false;
            B.GetComponent<Button>().interactable = false;
        }
            
        // Show graphically the wrong answers
        if (currentQuestion.correct == 0)
        {
            panelB.sprite = answerNo;
            panelC.sprite = answerNo;
        }
        else if (currentQuestion.correct == 1)
        {
            panelA.sprite = answerNo;
            panelC.sprite = answerNo;
        }
        else if (currentQuestion.correct == 2)
        {
            panelA.sprite = answerNo;
            panelB.sprite = answerNo;
        }

        if (currentQuestion.correct == answerInd)
        {
            // Answered correctly
            correctCounter++;
            // Change the blob image on breadcrumb panel
            var blob = blobGrid.transform.GetChild(questionCounter - 1);
            blob.GetComponent<Image>().sprite = blobYes;
            Debug.Log("voitit pelin");
        }
        else
        {
            // Answered poorly
            // Change the blob image on breadcrumb panel
            var blob = blobGrid.transform.GetChild(questionCounter - 1);
            blob.GetComponent<Image>().sprite = blobNo;
            Debug.Log("hihihii kutittaa");
        }

        var continueButton = GameObject.Find("ChalkBoard").transform.GetChild(4).gameObject;
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
        //string jsonString = System.IO.File.ReadAllText(Application.dataPath + "/Resources/JSON/questionBank.json");
        rootQuestion root = JsonUtility.FromJson<rootQuestion>(ladattava.text);
 

        foreach (var q in root.questions)
        {
            // new object and values from token
            questionList.Add(q);
        }
        printQuestionList();
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