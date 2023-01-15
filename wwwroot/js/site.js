// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const txHeight = 16;
const tx = document.getElementsByTagName("textarea");

for (let i = 0; i < tx.length; i++) {
    if (tx[i].value == '') {
        tx[i].setAttribute("style", "height:" + txHeight + "px;overflow-y:hidden; resize: none;");
    } else {
        tx[i].setAttribute("style", "height:" + (tx[i].scrollHeight) + "px;overflow-y:hidden; resize: none;");
    }
    tx[i].addEventListener("input", OnInput, false);
}

function OnInput() {
    this.style.height = 0;
    this.style.height = (this.scrollHeight) + "px";
}
