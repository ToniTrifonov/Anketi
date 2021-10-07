export function Element(type, textContent, attributes) {
    var element = document.createElement(type);
    element.textContent = textContent;

    if (attributes) {
        for (var attribute in attributes) {
            element.setAttribute(attribute, `${attributes[attribute]}`);
        }
    }

    return element;
}