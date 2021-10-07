import { Element } from '../shared/utility.js';

export function AddAnswer(target) {
    var answersCount = target.parentNode.getElementsByClassName('answer').length;

    var textareaElement = Element('textarea', '', {
        placeholder: 'Enter answer option here',
        id: `Questions[${target.parentNode.getAttribute('id')}].Answers[${answersCount}].Description`,
        name: `Questions[${target.parentNode.getAttribute('id')}].Answers[${answersCount}].Description`,
        class: 'questionAnswer'
    });
    var spanElement = Element('span', '', {
        class: 'alert-danger',
        'data-valmsg-for': `Questions[${target.parentNode.getAttribute('id')}].Answers[${answersCount}].Description`
    });
    var labelElement = Element('label', `Answer ${answersCount + 1}.`, {
        for: `Questions[${target.parentNode.getAttribute('id')}].Answers[${answersCount}].Description`
    });
    var cancelIconElement = Element('i', '', {
        class: 'fas fa-window-close remove-answer'
    });
    var answerElement = Element('article', '', {
        class: 'answer'
    });

    answerElement.appendChild(labelElement);
    answerElement.appendChild(textareaElement);
    answerElement.appendChild(spanElement);
    answerElement.appendChild(cancelIconElement);

    var answersSection = target.parentNode.querySelector('.create-form-questions-content-answers');

    answersSection.appendChild(answerElement);
}

export function RemoveAnswer(target) {
    var answerToRemove = target.parentNode;
    var questionNumber = Number(answerToRemove.parentNode.parentNode.getAttribute('id'));
    answerToRemove.remove();

    UpdateAnswers(answerToRemove, questionNumber);
}

export function UpdateAnswers(answerToRemove, answersQuestionNumber) {
    var answerLabel = answerToRemove.getElementsByTagName('label')[0];
    var answerLabelText = answerLabel.textContent;
    var answerOrderNumber = Number(answerLabelText.slice(6));

    [...document.getElementById(answersQuestionNumber).getElementsByClassName('answer')]
        .filter(answer => Number(answer.querySelector('label').textContent.slice(6)) > answerOrderNumber)
        .map(answer => {
            answer.querySelector('label').setAttribute('for', `Questions[${answersQuestionNumber}].Answers[${Number(answer.querySelector('label').textContent.slice(6)) - 2}].Description`);
            answer.querySelector('span').setAttribute('data-valmsg-for', `Questions[${answersQuestionNumber}].Answers[${Number(answer.querySelector('label').textContent.slice(6)) - 2}].Description`);
            answer.querySelector('textarea').setAttribute('name', `Questions[${answersQuestionNumber}].Answers[${Number(answer.querySelector('label').textContent.slice(6)) - 2}].Description`);
            answer.querySelector('textarea').setAttribute('id', `Questions[${answersQuestionNumber}].Answers[${Number(answer.querySelector('label').textContent.slice(6)) - 2}].Description`);
            answer.querySelector('label').textContent = `Answer ${Number(answer.querySelector('label').textContent.slice(6)) - 1}.`;
        });
}