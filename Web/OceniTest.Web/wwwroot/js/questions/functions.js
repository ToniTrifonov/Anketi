import { Element } from '../shared/utility.js'

export function AddQuestion(questionsCount) {
    var articleElement = Element('article', '', { class: 'question', id: `${questionsCount}` });

    var labelElement = Element('label', `Question ${questionsCount + 1}.`, { for: `Questions[${questionsCount}].Description` });
    var textareaElement = Element('textarea', '', { name: `Questions[${questionsCount}].Description`, id: `Questions[${questionsCount}].Description`, placeholder: 'Enter your question here' });
    var spanElement = Element('span', '', { 'data-valmsg-for': `Questions[${questionsCount}].Description` });
    var removeButtonElement = Element('button', 'Discard', { class: 'removeQuestionBtn' });
    var addQuestionAnswerElement = Element('button', 'Answer', { class: 'addAnswerBtn' });
    var answersElement = Element('section', '', { class: 'create-form-questions-content-answers' });

    articleElement.appendChild(labelElement);
    articleElement.appendChild(textareaElement);
    articleElement.appendChild(spanElement);
    articleElement.appendChild(removeButtonElement);
    articleElement.appendChild(addQuestionAnswerElement);
    articleElement.appendChild(answersElement);

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
            question.querySelector('textarea, label, span').setAttribute('name', `Questions[${question.getAttribute('id')}].Description`);
            question.querySelector('label').textContent = `Question ${Number(question.getAttribute('id')) + 1}.`;
        });
}