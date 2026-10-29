
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StackQuizManager : MonoBehaviour
{
    [Header("Quiz Panel")]
    public GameObject quizPanel;

    [Header("Text Elements")]
    public TMP_Text questionNumberText;
    public TMP_Text questionText;
    public TMP_Text feedbackText;
    public TMP_Text scoreText;

    [Header("Answer Buttons")]
    public Button[] answerButtons;

    [Header("Next Question")]
    public Button nextQuestionButton;
    public TMP_Text nextButtonText;

    private string[] questions =
    {
        "Which principle does a Stack follow?",
        "Which operation removes the TOP element from a Stack?",
        "What happens when PUSH is performed on a full Stack?"
    };

    private string[,] answers =
    {
        { "FIFO", "LIFO", "Random Access", "Priority Order" },
        { "PUSH", "PEEK", "POP", "ENQUEUE" },
        { "Stack Underflow", "Stack Overflow", "Stack Reset", "Stack Sorting" }
    };

    private int[] correctAnswers = { 1, 2, 1 };

    private int currentQuestion = 0;
    private int score = 0;
    private bool answered = false;

    // START QUIZ
    public void StartQuiz()
    {
        currentQuestion = 0;
        score = 0;
        answered = false;

        quizPanel.SetActive(true);

        ShowQuestion();
    }

    // DISPLAY QUESTION
    private void ShowQuestion()
    {
        answered = false;

        questionNumberText.text =
            "Question " + (currentQuestion + 1) + " / 3";

        questionText.text = questions[currentQuestion];

        feedbackText.text = "Select an answer";

        scoreText.text = "Score: " + score + " / 30";

        nextQuestionButton.gameObject.SetActive(false);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answerIndex = i;

            answerButtons[i].gameObject.SetActive(true);
            answerButtons[i].interactable = true;

            answerButtons[i]
                .GetComponentInChildren<TMP_Text>().text =
                (char)('A' + i) + ". " +
                answers[currentQuestion, i];

            answerButtons[i].onClick.RemoveAllListeners();

            answerButtons[i].onClick.AddListener(
                () => SelectAnswer(answerIndex)
            );
        }
    }

    // CHECK ANSWER
    public void SelectAnswer(int selectedAnswer)
    {
        if (answered)
            return;

        answered = true;

        if (selectedAnswer == correctAnswers[currentQuestion])
        {
            score += 10;
            feedbackText.text = "Correct! +10 points";
        }
        else
        {
            string correctAnswer =
                answers[
                    currentQuestion,
                    correctAnswers[currentQuestion]
                ];

            feedbackText.text =
                "Incorrect! Correct answer: " + correctAnswer;
        }

        scoreText.text = "Score: " + score + " / 30";

        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }

        nextQuestionButton.gameObject.SetActive(true);

        nextButtonText.text =
            currentQuestion == questions.Length - 1
                ? "VIEW RESULTS"
                : "NEXT QUESTION";
    }

    // NEXT QUESTION / TRY AGAIN
    public void NextQuestion()
    {
        // Restart quiz after showing results
        if (currentQuestion >= questions.Length)
        {
            StartQuiz();
            return;
        }

        if (!answered)
            return;

        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            ShowResults();
        }
    }

    // SHOW FINAL RESULTS
    private void ShowResults()
    {
        questionNumberText.text = "Quiz Completed!";

        questionText.text =
            "Your final score is " + score + " / 30";

        scoreText.text = "Score: " + score + " / 30";

        if (score == 30)
        {
            feedbackText.text =
                "Excellent! Perfect score!";
        }
        else if (score >= 20)
        {
            feedbackText.text = "Well done!";
        }
        else
        {
            feedbackText.text =
                "Keep practicing your Stack operations!";
        }

        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }

        nextButtonText.text = "TRY AGAIN";
        nextQuestionButton.gameObject.SetActive(true);
    }

    // OPTIONAL RESTART METHOD
    public void RestartQuiz()
    {
        StartQuiz();
    }

    // CLOSE QUIZ
    public void CloseQuiz()
    {
        quizPanel.SetActive(false);
    }
}
