using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionScript : MonoBehaviour {

    #region Properties
    public GameObject questionTextGO;
    public Sprite correctSprite, incorrectSprite;

    private int correctCounter = 0, questionCounter = 0;
    private Question currentQuestion;
    private GameObject A, B, C, pointsGO;
    private List<Question> questionList;
    private List<int> usedQuestions; //Maintain a list of the questions already used as to avoid duplicate questions during one run

#endregion

    #region Unity Monobehaviour
    // Use this for initialization
    void Start() {
        pointsGO = GameObject.Find("TextPoints");
        A = GameObject.Find("PanelAnswerA");
        B = GameObject.Find("PanelAnswerB");
        C = GameObject.Find("PanelAnswerC");
        questionList = new List<Question>();
        usedQuestions = new List<int>();
        questionTextGO = GameObject.Find("TextQuestion");
        fromJsonToList();
        // hae kysymys kysymyspankista
        getQuestion();
        // odota vastausta
    }

    private void Update()
    {
        pointsGO.GetComponent<Text>().text = "Kysymys " + questionCounter + "/10";
    }
#endregion

    #region Formatting methods
    /// <summary>
    /// Restart the quiz and reset correct counters and lists.
    /// </summary>
    public void restart()
    {
        var go = GameObject.Find("UICanvas").transform.GetChild(1).gameObject;
        go.SetActive(false);
        correctCounter = 0;
        questionCounter = 0;
        usedQuestions.Clear();
        getQuestion();
    }

    /// <summary>
    /// Call the upper level script to mark this one as done.
    /// </summary>
    public void quit()
    {
        Application.Quit(); //Call Mikko's upper scene after exiting this scene
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
        var panelA_Color = A.GetComponent<Image>();
        var panelB_Color = B.GetComponent<Image>();
        var panelC_Color = C.GetComponent<Image>();
        panelA_Color.color = Color.grey;
        panelB_Color.color = Color.grey;
        panelC_Color.color = Color.grey;

        if (questionCounter >= 1)
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
    /// 
    /// </summary>
    private void showResults()
    {
        var go = GameObject.Find("UICanvas").transform.GetChild(1).gameObject;
        go.SetActive(true);
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

        go.transform.GetChild(1).GetComponent<Text>().text = string.Format("Sait oikein {0}/10\n{1}", correctCounter, performance);

    }

    /// <summary>
    /// Draws the Question on the "board"
    /// </summary>
    /// <param name="que"></param>
    private void drawQuestion(Question que)
    {
        // Clear the markers from answers
        A.transform.GetChild(1).GetComponent<Image>().sprite = null;
        B.transform.GetChild(1).GetComponent<Image>().sprite = null;
        C.transform.GetChild(1).GetComponent<Image>().sprite = null;

        // jonkunlainen typewriter putkitushässäkkä
        var tque = questionTextGO.GetComponent<Text>();
        tque.text = que.question;
        A.transform.GetChild(0).GetComponent<Text>().text = que.answerA;
        B.transform.GetChild(0).GetComponent<Text>().text = que.answerB;
        C.transform.GetChild(0).GetComponent<Text>().text = que.answerC;
    }

    public void selectAnswer(int answerInd)
    {
        var panelA_Color = A.GetComponent<Image>();
        var panelB_Color = B.GetComponent<Image>();
        var panelC_Color = C.GetComponent<Image>();

        if (currentQuestion.correct == 0)
        {
            panelA_Color.color = Color.green;
            panelB_Color.color = Color.red;
            panelC_Color.color = Color.red;

            A.transform.GetChild(1).GetComponent<Image>().sprite = correctSprite;
            B.transform.GetChild(1).GetComponent<Image>().sprite = incorrectSprite;
            C.transform.GetChild(1).GetComponent<Image>().sprite = incorrectSprite;
        }
        else if (currentQuestion.correct == 1)
        {
            panelA_Color.color = Color.red;
            panelB_Color.color = Color.green;
            panelC_Color.color = Color.red;

            A.transform.GetChild(1).GetComponent<Image>().sprite = incorrectSprite;
            B.transform.GetChild(1).GetComponent<Image>().sprite = correctSprite;
            C.transform.GetChild(1).GetComponent<Image>().sprite = incorrectSprite;
        }
        else if (currentQuestion.correct == 2)
        {
            panelA_Color.color = Color.red;
            panelB_Color.color = Color.red;
            panelC_Color.color = Color.green;

            A.transform.GetChild(1).GetComponent<Image>().sprite = incorrectSprite;
            B.transform.GetChild(1).GetComponent<Image>().sprite = incorrectSprite;
            C.transform.GetChild(1).GetComponent<Image>().sprite = correctSprite;
        }

        if (currentQuestion.correct == answerInd)
        {
            // Answered correctly
            correctCounter++;
            Debug.Log("voitit pelin");
        }
        else
        {
            // Answered poorly
            Debug.Log("hihihii kutittaa");
        }

        var x = GameObject.Find("ChalkBoard").transform.GetChild(4).gameObject;
        x.SetActive(true);
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