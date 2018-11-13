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
        // Question question = getQuestion();
        // piirrä kysymys canvasille
        //drawQuestion(question);
        // odota vastausta
    }

    /// <summary>
    /// Gets a Question from the
    /// </summary>
    /// <returns></returns>
    private Question getQuestion()
    {
        return questionList[0];
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
            Question que = new Question();
            que.question = q.Value<string>("question");
            que.answerA = q.Value<string>("answerA");
            que.answerB = q.Value<string>("answerB");
            que.answerC = q.Value<string>("answerC");
            que.correct = q.Value<int>("correct");
            questionList.Add(que);
        }
        printQuestionList();
    }

    /// <summary>
    /// Debug printing for questionList. Prints all questions in list.
    /// </summary>
    private void printQuestionList()
    {
        int i = 0;
        foreach (var q in questionList)
        {
            Debug.Log("Question #" + i);
            Debug.Log(q.ToString());
        }
    }

    /// <summary>
    /// Draws the Question on the "board"
    /// </summary>
    /// <param name="que"></param>
    private void drawQuestion(Question que)
    {
        // jonkunlainen typewriter putkitushässäkkä
        questionTextGO.GetComponent<Text>().text = que.question;
        var ansList = questionTextGO.GetComponentsInChildren<Text>();
        GameObject.Find("TextAnswerA").GetComponent<Text>().text = que.answerA;
        GameObject.Find("TextAnswerB").GetComponent<Text>().text = que.answerB;
        GameObject.Find("TextAnswerC").GetComponent<Text>().text = que.answerC;
    }

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
            return string.Format("\nQ: {0}, #0: {1}, #1: {2}, #2: {3}, CORRECT: {4}",
                                 question, answerA, answerB, answerC, correct);
        }
    }
}
