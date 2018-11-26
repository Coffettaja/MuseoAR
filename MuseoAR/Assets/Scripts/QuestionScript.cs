using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class QuestionScript : MonoBehaviour {

    
    public GameObject questionTextGO;

    private List<Question> questionList;

    // Use this for initialization
    void Start() {
        questionList = new List<Question>();
        questionTextGO = GameObject.Find("TextQuestion");
        fromJsonToList();
        // hae kysymys kysymyspankista
        Question question = getQuestion();
        // piirrä kysymys canvasille
        drawQuestion(question);
        // odota vastausta
    }

    /// <summary>
    /// Gets a random (TODO: non-asked) Question from the list of questions
    /// </summary>
    /// <returns></returns>
    private Question getQuestion()
    {
        var rng = UnityEngine.Random.Range(0, questionList.Count);
        return questionList[rng];
    }

    /// <summary>
    /// Adds all entries from questionBank.json to questionList as Question objects.
    /// </summary>
    private void fromJsonToList()
    {
        // parsing the json into token array
        string jsonString = System.IO.File.ReadAllText("Assets/Resources/JSON/questionBank.json");
        var parsed = Newtonsoft.Json.Linq.JObject.Parse(jsonString);
        var questionArr = parsed.GetValue("questions").Children();

        foreach (var q in questionArr)
        {
            // new object and values from token
            Question que = new Question();
            que = q.ToObject<Question>();
            questionList.Add(que);
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
    /// Draws the Question on the "board"
    /// </summary>
    /// <param name="que"></param>
    private void drawQuestion(Question que)
    {
        // jonkunlainen typewriter putkitushässäkkä
        var tque = questionTextGO.GetComponent<Text>();
        tque.text = que.question;
        var A = GameObject.Find("TextAnswerA").GetComponent<Text>();
        var B = GameObject.Find("TextAnswerB").GetComponent<Text>();
        var C = GameObject.Find("TextAnswerC").GetComponent<Text>();
        A.text = que.answerA;
        B.text = que.answerB;
        C.text = que.answerC;

        // tee jotain oikealle? ei ehkä tähän
        if (que.correct == 0)
            A.color = Color.green;
        if (que.correct == 1)
            B.color = Color.green;
        if (que.correct == 2)
            C.color = Color.green;
    }

    /// <summary>
    /// Question class for storing questiondata.
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
}
