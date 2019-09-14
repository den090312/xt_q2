window.onload = function () {
    var options = document.getElementsByClassName("options"); 
    if (options) {
        proceedToOptions(options);
    }
}

function proceedToOptions(options) {
    for (let option of options) {
        var buttons = option.getElementsByClassName("button");
        if (buttons) {
            for (let button of buttons) {
                if (button) {
                    buttonOnclick(button);
                }
            }
        }
    }
}

function buttonOnclick(button) {
    button.onclick = function(event) {
        var target = event.target;
        if (target) {
            switch (target.className) {
                case "user_create":
                    alert("user_create");
                    break;
                case "user_delete":
                    alert("user_delete");
                    break;
                case "user_print":
                    alert("user_print");
                    break;
                case "award_create":
                    alert("award_create");
                    break;
                case "award_delete":
                    alert("award_delete");
                    break;
                case "award_print":
                    alert("award_print");
                    break;
                case "join":
                    alert("join");
                    break;
                case "exit":
                    alert("exit");
                    break;
            }
        }
    };
}
