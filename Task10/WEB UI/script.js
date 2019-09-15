window.onload = function () {
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
                proceedToButtonOnclick();
            }
        }

        function proceedToButtonOnclick() {
            for (let button of buttons) {
                if (button) {
                    buttonOnclick(button, operations);
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
            var main = document.getElementById("main");
            var target = event.target;
            if (target) {
                switch (target.className) {
                    case "cancel":
                        cancelOperation(main, operation);
                        break;
                    case "user_create":
                        userCreation(main);
                        break;
                    case "award_create":
                        awardCreation(main);
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
                alertbox.innerHTML = "Date of birth can't be more than current date!";
                return;
            }
            goToMain(main);
        }

        function awardCreation(main) {
            var award_creation = document.getElementsByClassName("award_creation")[0];
            var alertbox = user_creation.getElementsByClassName("alertbox")[0];
            alertbox.style.display = "none";
            var inputtitle = award_creation.getElementsByClassName("title")[0];
            if (!inputtitle.value) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Title is missing!";
                return;
            }
            goToMain(main);
        }

        function goToMain(main) {
            operation.style.display = "none";
            main.style.display = "block";
        }

        function cancelOperation(main, operation) {
            operation.style.display = "none";
            main.style.display = "block";
        }
    }

    function buttonOnclick(button, operations) {
        button.onclick = function (event) {
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
                        proceedToAwardCreation(main);
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
            var award_creation = document.getElementsByClassName("award_creation")[0];
            award_creation.style.display = "none";
            var inputname = user_creation.getElementsByClassName("name")[0];
            inputname.value = "";
            var dateOfBirth = user_creation.getElementsByClassName("dateOfBirth")[0];
            dateOfBirth.value = "";
        }

        function proceedToAwardCreation(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            var user_creation = document.getElementsByClassName("user_creation")[0];
            user_creation.style.display = "none";
            var award_creation = document.getElementsByClassName("award_creation")[0];
            award_creation.style.display = "block";
            var inputtitle = award_creation.getElementsByClassName("title")[0];
            inputtitle.value = "";
        }
    }
}