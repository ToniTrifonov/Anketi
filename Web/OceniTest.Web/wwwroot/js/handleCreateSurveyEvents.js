import { AddQuestion, RemoveQuestion } from './questions/functions.js'
import { AddAnswer, RemoveAnswer } from './answers/functions.js'

var questionsCount = document.getElementsByClassName('question').length;
var answersCount = document.getElementsByClassName('answer').length;

document.getElementById('addQuestionBtn').addEventListener('click', function (event) {
    event.preventDefault();

    AddQuestion(questionsCount);

    questionsCount++;
})

document.getElementById('Questions').addEventListener('click', function (event) {
    event.preventDefault();
    var target = event.target;

    if (target.textContent == 'Discard') {
        RemoveQuestion(target);

        questionsCount--;
    } else if (target.textContent == 'Answer') {
        AddAnswer(target);
    } else if (target.classList.contains('remove-answer')) {
        RemoveAnswer(target);
    }
})
