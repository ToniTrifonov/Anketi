import { AddQuestion, RemoveQuestion } from './questions/functions_v2.js'
import { AddAnswer, RemoveAnswer } from './answers/functions_v2.js'

var questionsCount = document.getElementsByClassName('question').length;
var answersCount = document.getElementsByClassName('answer').length;

document.getElementById('addQuestionBtn').addEventListener('click', function (event) {
    event.preventDefault();

    AddQuestion(questionsCount, false);

    questionsCount++;
})

document.getElementById('Questions').addEventListener('click', function (event) {
    event.preventDefault();
    var target = event.target;

    if (target.textContent == 'Discard')
    {
        RemoveQuestion(target);

        questionsCount--;
    }
    else if (target.textContent == 'Answer')
    {
        AddAnswer(target);
    }
    else if (target.classList.contains('remove-answer'))
    {
        RemoveAnswer(target);
    }
})
