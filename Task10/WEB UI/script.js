window.onload = function () {
    var options = document.getElementsByClassName("options");
    var operations = document.getElementsByClassName("operations");
    if (options) {
        proceedToOptions(options, operations);
    }
    if (operations) {
        proceedToOperations(operations);
    }
    setSelectedUserGuid();
    setSelectedAwardGuid();

    function setSelectedUserGuid() {
        var user_chosen = document.getElementsByClassName("user_chosen")[0];
        var user_chosen_guid = document.getElementsByClassName("user_chosen_guid")[0];
        var user_guid = document.getElementsByClassName("user_guid")[0];
        user_guid.value = user_chosen_guid[user_chosen_guid.selectedIndex].value;
        user_chosen.onchange = function () {
            user_chosen_guid.selectedIndex = user_chosen.selectedIndex;
            user_guid.value = user_chosen_guid[user_chosen_guid.selectedIndex].value;
        };
    }

    function setSelectedAwardGuid() {
        var award_chosen = document.getElementsByClassName("award_chosen")[0];
        var award_chosen_guid = document.getElementsByClassName("award_chosen_guid")[0];
        var award_guid = document.getElementsByClassName("award_guid")[0];
        award_guid.value = award_chosen_guid[award_chosen_guid.selectedIndex].value;
        award_chosen.onchange = function () {
            award_chosen_guid.selectedIndex = award_chosen.selectedIndex;
            award_guid.value = award_chosen_guid[award_chosen_guid.selectedIndex].value;
        };
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
                        goToMain(main);
                        break;
                    case "user_create":
                        userCreation(main);
                        break;
                    case "user_delete":
                        goToMain(main);
                        break;
                    case "award_create":
                        awardCreation(main);
                        break;
                    case "award_delete":
                        goToMain(main);
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
    }

    function buttonOnclick(button, operations) {
        button.onclick = function (event) {
            var main = document.getElementById("main");
            var target = event.target;
            if (target) {
                switch (target.className) {
                    case "user_create":
                        userCreate(main);
                        break;
                    case "user_delete":
                        userDelete(main);
                        break;
                    case "user_print":
                        printUsers();
                        break;
                    case "award_create":
                        awardCreate(main);
                        break;
                    case "award_delete":
                        awardDelete(main);
                        break;
                    case "award_print":
                        printAwards(main);
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

        function userDelete(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            var user_del = document.getElementsByClassName("user_del")[0];
            user_del.style.display = "block";
            var award_del = document.getElementsByClassName("award_del")[0];
            award_del.style.display = "none";
            var user_creation = document.getElementsByClassName("user_creation")[0];
            user_creation.style.display = "none";
            var award_creation = document.getElementsByClassName("award_creation")[0];
            award_creation.style.display = "none";
            var listusers = document.getElementsByClassName("listusers")[0];
            listusers.style.display = "none";
            var listawards = document.getElementsByClassName("listawards")[0];
            listawards.style.display = "none";
        }

        function awardDelete(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            var user_del = document.getElementsByClassName("user_del")[0];
            user_del.style.display = "none";
            var award_del = document.getElementsByClassName("award_del")[0];
            award_del.style.display = "block";
            var user_creation = document.getElementsByClassName("user_creation")[0];
            user_creation.style.display = "none";
            var award_creation = document.getElementsByClassName("award_creation")[0];
            award_creation.style.display = "none";
            var listusers = document.getElementsByClassName("listusers")[0];
            listusers.style.display = "none";
            var listawards = document.getElementsByClassName("listawards")[0];
            listawards.style.display = "none";
        }

        function printAwards(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            var user_del = document.getElementsByClassName("user_del")[0];
            user_del.style.display = "none";
            var award_del = document.getElementsByClassName("award_del")[0];
            award_del.style.display = "none";
            var user_creation = document.getElementsByClassName("user_creation")[0];
            user_creation.style.display = "none";
            var award_creation = document.getElementsByClassName("award_creation")[0];
            award_creation.style.display = "none";
            var listusers = document.getElementsByClassName("listusers")[0];
            listusers.style.display = "none";
            var listawards = document.getElementsByClassName("listawards")[0];
            listawards.style.display = "block";
        }

        function printUsers() {
            main.style.display = "none";
            operations[0].style.display = "block";
            var user_del = document.getElementsByClassName("user_del")[0];
            user_del.style.display = "none";
            var award_del = document.getElementsByClassName("award_del")[0];
            award_del.style.display = "none";
            var user_creation = document.getElementsByClassName("user_creation")[0];
            user_creation.style.display = "none";
            var award_creation = document.getElementsByClassName("award_creation")[0];
            award_creation.style.display = "none";
            var listusers = document.getElementsByClassName("listusers")[0];
            listusers.style.display = "block";
            var listawards = document.getElementsByClassName("listawards")[0];
            listawards.style.display = "none";
        }

        function userCreate(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            var user_del = document.getElementsByClassName("user_del")[0];
            user_del.style.display = "none";
            var award_del = document.getElementsByClassName("award_del")[0];
            award_del.style.display = "none";
            var user_creation = document.getElementsByClassName("user_creation")[0];
            user_creation.style.display = "block";
            var award_creation = document.getElementsByClassName("award_creation")[0];
            award_creation.style.display = "none";
            var listusers = document.getElementsByClassName("listusers")[0];
            listusers.style.display = "none";
            var listawards = document.getElementsByClassName("listawards")[0];
            listawards.style.display = "none";
        }

        function awardCreate(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            var user_del = document.getElementsByClassName("user_del")[0];
            user_del.style.display = "none";
            var award_del = document.getElementsByClassName("award_del")[0];
            award_del.style.display = "none";
            var user_creation = document.getElementsByClassName("user_creation")[0];
            user_creation.style.display = "none";
            var award_creation = document.getElementsByClassName("award_creation")[0];
            award_creation.style.display = "block";
            var listusers = document.getElementsByClassName("listusers")[0];
            listusers.style.display = "none";
            var listawards = document.getElementsByClassName("listawards")[0];
            listawards.style.display = "none";
        }
    }
}