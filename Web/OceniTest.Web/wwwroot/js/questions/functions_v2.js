import { Element } from '../shared/utility.js'
import { AddAnswer } from '../answers/functions_v2.js'

export function AddQuestion(questionsCount, isOpenEnded) {
    var articleElement = Element('article', '', { class: 'question', id: `${questionsCount}` });

    var labelElement = Element('label', `Question ${questionsCount + 1}.`, { for: `Questions[${questionsCount}].Description` });
    var textareaElement = Element('textarea', '', { name: `Questions[${questionsCount}].Description`, id: `Questions[${questionsCount}].Description`, placeholder: 'Enter your question here' });
    var removeButtonElement = Element('button', 'Discard', { class: 'removeQuestionBtn' });
    var answersElement = Element('section', '', { class: 'create-form-questions-content-answers' });

    articleElement.appendChild(labelElement);
    articleElement.appendChild(removeButtonElement);
    articleElement.appendChild(textareaElement);

    articleElement.appendChild(answersElement);

    if (isOpenEnded)
    {
        AddAnswer(answersElement);
    }
    else
    {
        AddAnswer(answersElement);
        AddAnswer(answersElement);
        var spanElement = Element('span', '', { 'data-valmsg-for': `Questions[${questionsCount}].Description` });
        var addQuestionAnswerElement = Element('button', 'Answer', { class: 'addAnswerBtn' });

        articleElement.appendChild(spanElement);
        articleElement.appendChild(addQuestionAnswerElement);
    }

    document.querySelector('#Questions').appendChild(articleElement);
}

export function RemoveQuestion(target) {
    var questionToRemove = target.parentNode;
    questionToRemove.remove();

    UpdateQuestions(questionToRemove);
}

export function UpdateQuestions(questionToRemove) {
    var questionNumber = questionToRemove.getAttribute('id');

    [...document.getElementsByClassName('question')]
        .filter(question => Number(question.getAttribute('id')) > questionNumber)
        .map(question => {
            question.setAttribute('id', question.getAttribute('id') - 1);
            question.querySelector('label').setAttribute('for', `Questions[${question.getAttribute('id')}].Description`);
            question.querySelector('textarea').setAttribute('name', `Questions[${question.getAttribute('id')}].Description`);
            question.querySelector('textarea').setAttribute('id', `Questions[${question.getAttribute('id')}].Description`);
            question.querySelector('span').setAttribute('data-valmsg-for', `Questions[${question.getAttribute('id')}].Description`);
            question.querySelector('label').textContent = `Question ${Number(question.getAttribute('id')) + 1}.`;
        });
}