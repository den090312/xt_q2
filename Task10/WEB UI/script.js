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
    setJoinGuidUser();
    setJoinGuidAward();

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

    function setJoinGuidUser() {
        var user_chosen_join = document.getElementsByClassName("user_chosen_join")[0];
        var user_chosen_join_guid = document.getElementsByClassName("user_chosen_join_guid")[0];
        var user_guid_join = document.getElementsByClassName("user_guid_join")[0];
        user_guid_join.value = user_chosen_join_guid[user_chosen_join_guid.selectedIndex].value;
        user_chosen_join.onchange = function () {
            user_chosen_join_guid.selectedIndex = user_chosen_join.selectedIndex;
            user_guid_join.value = user_chosen_join_guid[user_chosen_join_guid.selectedIndex].value;
        }
    }

    function setJoinGuidAward() {
        var award_chosen_join = document.getElementsByClassName("award_chosen_join")[0];
        var award_chosen_join_guid = document.getElementsByClassName("award_chosen_join_guid")[0];
        var award_guid_join = document.getElementsByClassName("award_guid_join")[0];
        award_guid_join.value = award_chosen_join_guid[award_chosen_join_guid.selectedIndex].value;
        award_chosen_join.onchange = function () {
            award_chosen_join_guid.selectedIndex = award_chosen_join.selectedIndex;
            award_guid_join.value = award_chosen_join_guid[award_chosen_join_guid.selectedIndex].value;
        }
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
                        if (!confirm("Награда будет удалена у всех пользователей. Вы уверены?")) {
                            document.getElementsByClassName("award_guid")[0].value = "";
                            goToMain(main);
                        }
                        break;
                    case "join_user_award":
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
                        DisplayUserCreation(main);
                        break;
                    case "user_delete":
                        DisplayUserDel(main);
                        break;
                    case "user_print":
                        DisplayListUsers();
                        break;
                    case "award_create":
                        DisplayAwardCreation(main);
                        break;
                    case "award_delete":
                        DisplayAwardDel(main);
                        break;
                    case "award_print":
                        DisplayListAwards(main);
                        break;
                    case "join_button":
                        DisplayJoin(main);
                        break;
                    case "user_edit":
                        DisplayUserEdition(main);
                        break;
                    case "award_edit":
                        DisplayAwardEdition(main);
                        break;
                }
            }
        };

        function DisplayUserDel(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "block";
            document.getElementsByClassName("award_del")[0].style.display = "none";
            document.getElementsByClassName("user_creation")[0].style.display = "none";
            document.getElementsByClassName("award_creation")[0].style.display = "none";
            document.getElementsByClassName("listusers")[0].style.display = "none";
            document.getElementsByClassName("listawards")[0].style.display = "none";
            document.getElementsByClassName("join")[0].style.display = "none";
            document.getElementsByClassName("user_edition")[0].style.display = "none";
            document.getElementsByClassName("award_edition")[0].style.display = "none";
        }

        function DisplayAwardDel(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "none";
            document.getElementsByClassName("award_del")[0].style.display = "block";
            document.getElementsByClassName("user_creation")[0].style.display = "none";
            document.getElementsByClassName("award_creation")[0].style.display = "none";
            document.getElementsByClassName("listusers")[0].style.display = "none";
            document.getElementsByClassName("listawards")[0].style.display = "none";
            document.getElementsByClassName("join")[0].style.display = "none";
            document.getElementsByClassName("user_edition")[0].style.display = "none";
            document.getElementsByClassName("award_edition")[0].style.display = "none";
        }

        function DisplayListAwards(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "none";
            document.getElementsByClassName("award_del")[0].style.display = "none";
            document.getElementsByClassName("user_creation")[0].style.display = "none";
            document.getElementsByClassName("award_creation")[0].style.display = "none";
            document.getElementsByClassName("listusers")[0].style.display = "none";
            document.getElementsByClassName("listawards")[0].style.display = "block";
            document.getElementsByClassName("join")[0].style.display = "none";
            document.getElementsByClassName("user_edition")[0].style.display = "none";
            document.getElementsByClassName("award_edition")[0].style.display = "none";
        }

        function DisplayListUsers() {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "none";
            document.getElementsByClassName("award_del")[0].style.display = "none";
            document.getElementsByClassName("user_creation")[0].style.display = "none";
            document.getElementsByClassName("award_creation")[0].style.display = "none";
            document.getElementsByClassName("listusers")[0].style.display = "block";
            document.getElementsByClassName("listawards")[0].style.display = "none";
            document.getElementsByClassName("join")[0].style.display = "none";
            document.getElementsByClassName("user_edition")[0].style.display = "none";
            document.getElementsByClassName("award_edition")[0].style.display = "none";
        }

        function DisplayUserCreation(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "none";
            document.getElementsByClassName("award_del")[0].style.display = "none";
            document.getElementsByClassName("user_creation")[0].style.display = "block";
            document.getElementsByClassName("award_creation")[0].style.display = "none";
            document.getElementsByClassName("listusers")[0].style.display = "none";
            document.getElementsByClassName("listawards")[0].style.display = "none";
            document.getElementsByClassName("join")[0].style.display = "none";
            document.getElementsByClassName("user_edition")[0].style.display = "none";
            document.getElementsByClassName("award_edition")[0].style.display = "none";
        }

        function DisplayAwardCreation(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "none";
            document.getElementsByClassName("award_del")[0].style.display = "none";
            document.getElementsByClassName("user_creation")[0].style.display = "none";
            document.getElementsByClassName("award_creation")[0].style.display = "block";
            document.getElementsByClassName("listusers")[0].style.display = "none";
            document.getElementsByClassName("listawards")[0].style.display = "none";
            document.getElementsByClassName("join")[0].style.display = "none";
            document.getElementsByClassName("user_edition")[0].style.display = "none";
            document.getElementsByClassName("award_edition")[0].style.display = "none";
        }

        function DisplayJoin(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "none";
            document.getElementsByClassName("award_del")[0].style.display = "none";
            document.getElementsByClassName("user_creation")[0].style.display = "none";
            document.getElementsByClassName("award_creation")[0].style.display = "none";
            document.getElementsByClassName("listusers")[0].style.display = "none";
            document.getElementsByClassName("listawards")[0].style.display = "none";
            document.getElementsByClassName("join")[0].style.display = "block";
            document.getElementsByClassName("user_edition")[0].style.display = "none";
            document.getElementsByClassName("award_edition")[0].style.display = "none";
        }

        function DisplayUserEdition(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "none";
            document.getElementsByClassName("award_del")[0].style.display = "none";
            document.getElementsByClassName("user_creation")[0].style.display = "none";
            document.getElementsByClassName("award_creation")[0].style.display = "none";
            document.getElementsByClassName("listusers")[0].style.display = "none";
            document.getElementsByClassName("listawards")[0].style.display = "none";
            document.getElementsByClassName("join")[0].style.display = "none";
            document.getElementsByClassName("user_edition")[0].style.display = "block";
            document.getElementsByClassName("award_edition")[0].style.display = "none";
        }

        function DisplayAwardEdition(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "none";
            document.getElementsByClassName("award_del")[0].style.display = "none";
            document.getElementsByClassName("user_creation")[0].style.display = "none";
            document.getElementsByClassName("award_creation")[0].style.display = "none";
            document.getElementsByClassName("listusers")[0].style.display = "none";
            document.getElementsByClassName("listawards")[0].style.display = "none";
            document.getElementsByClassName("join")[0].style.display = "none";
            document.getElementsByClassName("user_edition")[0].style.display = "none";
            document.getElementsByClassName("award_edition")[0].style.display = "block";
        }
    }
}