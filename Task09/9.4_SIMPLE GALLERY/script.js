RunCountdown();

var paused = false;

function RunCountdown()
{
    var seconds = 10;

    var interval = setInterval(function() 
    {
        if (!paused)
        {
            document.getElementById("timer").innerHTML = seconds;
            seconds--;
        
            if (seconds < 0) 
            {
                clearInterval(interval);
                GoToLocation();
            }
        }
     
    }, 1000);
}

function CountdownAction(obj)
{
    var id = obj.id;
    document.getElementById(id).style.display = "none";

    if (id == "countdownActionPause")
    {
        paused = true;
        document.getElementById("countdownActionPlay").style.display = "block";
    }
    else
    {
        paused = false;
        document.getElementById("countdownActionPause").style.display = "block";
    }
}

function GoToLocation()
{
    var pageName = GetCurrentPageName(window.location);

    if (pageName == "index")
    {
        window.location.href = "1.html";    
    }
    else if (pageName == "5")
    {
        if (confirm("Continue?")) 
        {
            window.location.href = "index.html";  
        } 
        else 
        {
            window.open('', '_self', ''); 
            window.close();    
        }     
    }
    else
    {
        GoToNextPage(pageName);
    }
}

function GoToPrevPage()
{
    var pageName = GetCurrentPageName(window.location); 

    if (pageName == 1)
    {
        window.location.href = "index.html";          
    }
    else
    {
        var nextPageNumber = Number(pageName) - 1; 

        window.location.href = nextPageNumber + ".html";  
    } 
}

function GoToNextPage(pageName)
{
    var nextPageNumber = Number(pageName) + 1; 

    window.location.href = nextPageNumber + ".html"; 
}


function GetCurrentPageName(location)
{
    return location.pathname.split("/").pop().split(".").shift(); 
}