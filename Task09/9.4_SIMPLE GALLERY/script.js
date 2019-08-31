var paused = false;

RunCountdown();

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

function GoToLocation()
{
    var pageName = GetCurrentPageName(window.location);

    if (pageName == "index")
    {
        GoToFirstPage();    
    }
    else if (pageName == "5")
    {
        ConfirmContinue();    
    }
    else
    {
        GoToNextPage(pageName);
    }
}

function CountdownPause(obj)
{
    HideCurrentButton(obj.id);
    paused = true;
    DisplayButtonPlay();
}

function CountdownResume(obj)
{
    HideCurrentButton(obj.id);
    paused = false;
    DisplayButtonPause();
}

function ActionPrevPage()
{
    var pageName = GetCurrentPageName(window.location); 

    pageName == 1 ? GoToIndexPage() : GoToPrevPage(pageName);  
}

function ConfirmContinue()
{
    confirm("Continue?") ? GoToIndexPage() : CloseCurrentTab();  
}

function DisplayButtonPlay()
{
    document.getElementById("countdownActionResume").style.display = "block";    
}

function DisplayButtonPause()
{
    document.getElementById("countdownActionPause").style.display = "block";    
}

function GoToPrevPage(pageName)
{
    var nextPageNumber = Number(pageName) - 1; 

    window.location.href = nextPageNumber + ".html"; 
}

function GoToNextPage(pageName)
{
    var nextPageNumber = Number(pageName) + 1; 

    window.location.href = nextPageNumber + ".html"; 
}

function GoToIndexPage()
{
    window.location.href = "index.html";     
}

function GoToFirstPage()
{
    window.location.href = "1.html"; 
}

function GetCurrentPageName(location)
{
    return location.pathname.split("/").pop().split(".").shift(); 
}

function HideCurrentButton(id)
{
    document.getElementById(id).style.display = "none";
}

function CloseCurrentTab()
{
    window.open('', '_self', ''); 
    window.close();     
}