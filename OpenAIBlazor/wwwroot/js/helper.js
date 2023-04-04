function removeImages() {
    var images = document.getElementsByTagName("img");
    for (var i = 0; i < images.length; i++) {
        images[i].remove();
    }
}
function ClearInputText() {
    let txtbox = document.getElementById("txtInput");
    txtbox.value = "";
}

function scrollIntoView() {
    var target = document.getElementById("txtInput");
    target.value = "";
    target.scrollIntoView({ behavior: "smooth", block: "end" });
    target.focus();
}


function CreateQuestion() {
    //Get Parent div
    let divContainer = document.getElementById("divContainer");

    const ulElement = document.getElementById('ul-main');
    const question = document.getElementById('txtInput').value;

    //Create a new list element
    let newLI = document.createElement("li");
    newLI.setAttribute("class", "message left");
/*    newLI.setAttribute("class", "message-question");*/
    newLI.setAttribute("b-kfox5zgk4m", "");    /* WHY DO I HAVE TO ADD THIS ????? */

    //  Create a new image element and set its source
    let newQuestionImg = document.createElement("img");
    newQuestionImg.setAttribute("class", "logo");
    //newQuestionImg.setAttribute("class", "message-question-logo");
   // newQuestionImg.setAttribute("src", "images/Tor1.jpg");
    newQuestionImg.setAttribute("src", "images/qa.png");
    newQuestionImg.setAttribute("alt", "");
    newQuestionImg.setAttribute("b-kfox5zgk4m", "");  /* WHY DO I HAVE TO ADD THIS ????? */

    //  Create a new paragraph for the question
    //  add some attributes for ID and Class and
    //  append it to the parent div
    let newPQ = document.createElement("p");
    //newPQ.setAttribute("class", "message-question-p");
    newPQ.innerHTML = question;
    newPQ.setAttribute("b-kfox5zgk4m", "");  /* WHY DO I HAVE TO ADD THIS ????? */

    newLI.appendChild(newQuestionImg);
    newLI.appendChild(newPQ);

    ulElement.appendChild(newLI);
    divContainer.scrollIntoView({ behavior: "smooth", block: "end" });

}


function typeOutWords(words) {

    //let i = 0;
  
    //Get Parent div
    let divContainer = document.getElementById("divMessageSection");

    //Get a existing ul element
    const ulElement = document.getElementById('ul-main');

    //Create a new list element
    let newLI = document.createElement("li");
    newLI.setAttribute("class", "message right");
   /* newLI.setAttribute("value", "@shouldRender");*/
    newLI.setAttribute("bind-value", "@shouldRender");

/*    newLI.setAttribute("class", "message-answer");*/
  //  newLI.setAttribute("b-kfox5zgk4m", "");  /* WHY DO I HAVE TO ADD THIS ????? */

    //  Create a new image element and set its source
    let newAnswerImg = document.createElement("img");
    newAnswerImg.setAttribute("class", "logo");
    //newAnswerImg.setAttribute("class", "message-answer-logo");
    newAnswerImg.setAttribute("src", "images/answer1.png");
    newAnswerImg.setAttribute("alt", "");
   // newAnswerImg.setAttribute("b-kfox5zgk4m", ""); /* WHY DO I HAVE TO ADD THIS ????? */


    // Create a new paragraph element and populate it 
    // with answer coming back from ChatGPT
    let newPA = document.createElement("p");
   // newPA.setAttribute("class", "message-answer-p");
 //   newPA.setAttribute("b-kfox5zgk4m", ""); /* WHY DO I HAVE TO ADD THIS ????? */

        let arr = words.split(/[\n\r]/);
        let icount = 0;
        let timer = setInterval(function () {
            if (icount >= arr.length) {
                clearInterval(timer);
                return;
            }
            newPA.innerHTML += arr[icount] + "<br>";
            icount++;
        }, 100); // Delay between each word in milliseconds

    //Append the image and paragraph to the new li element
    newLI.appendChild(newAnswerImg);
    newLI.appendChild(newPA);

   //Append the the new li element to the ul element
    ulElement.appendChild(newLI);

   // location.reload(); /* This line will refresh the Browser page 
    divContainer.scrollIntoView({ behavior: "smooth", block: "end" });
   

}


