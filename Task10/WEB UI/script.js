window.onload = function () {
    var message_done = document.getElementById("message_done");
    var message_failure = document.getElementById("message_failure");

    function disableMessages() {
        message_done.style.display = "none";
        message_failure.style.display = "none";
    }

    var options = document.getElementsByClassName("options"); 
    var operations = document.getElementsByClassName("operations");
    if (options) {
        proceedToOptions(options, operations);
    }
    if (operations) {
        proceedToOperations(operations);
    }

    function proceedToOptions(options, operations) {
        for (let option of options) {
            var buttons = option.getElementsByClassName("button");
            if (buttons) {
                for (let button of buttons) {
                    if (button) {
                        buttonOnclick(button, operations);
                    }
                }
            }
        }
    }

    function proceedToOperations(operations) {
        for (let operation of operations) {
            if (operation) {
                operationOnClick(operation);
            }
        }
    }

    function operationOnClick(operation) {
        operation.onclick = function (event) {
            disableMessages();
            var main = document.getElementById("main");
            var target = event.target;
            if (target) {
                switch (target.className) {
                    case "cancel":
                        cancelOperation(main);
                        break;
                    case "user_create":
                        userCreation(main);
                        break;
                }
            }
        }

        function userCreation(main) {
            var user_creation = document.getElementsByClassName("user_creation")[0];
            var alertbox = user_creation.getElementsByClassName("alertbox")[0];
            alertbox.style.display = "none";
            var inputname = user_creation.getElementsByClassName("name")[0];
            if (!inputname.value) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Name is missing!";
                return;
            }
            var dateOfBirth = user_creation.getElementsByClassName("dateOfBirth")[0];
            if (!dateOfBirth.value) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Date of birth is missing!";
                return;
            }
            else if (new Date(dateOfBirth.value) >= new Date()) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Date of birth is not correct!";
                return;
            }
            goToMain(main);
        }

        function goToMain(main) {
            operation.style.display = "none";
            message_done.style.display = "block";
            main.style.display = "block";
        }

        function cancelOperation(main) {
            operation.style.display = "none";
            main.style.display = "block";
        }
    }

    function buttonOnclick(button, operations) {
        button.onclick = function (event) {
            disableMessages();
            var main = document.getElementById("main");
            var target = event.target;
            if (target) {
                switch (target.className) {
                    case "user_create":
                        proceedTouserCreation(main);
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

        function proceedTouserCreation(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            var user_creation = document.getElementsByClassName("user_creation")[0];
            user_creation.style.display = "block";
            var inputname = user_creation.getElementsByClassName("name")[0];
            inputname.value = "";
            var dateOfBirth = user_creation.getElementsByClassName("dateOfBirth")[0]; 
            dateOfBirth.value = "";
        }
    }
}