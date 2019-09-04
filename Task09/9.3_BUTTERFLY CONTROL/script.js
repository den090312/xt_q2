window.onload = function()
{	
	var butterflyList = document.getElementsByClassName("butterfly"); 

	if (butterflyList)
	{
		for (let butterfly of butterflyList)
		{
			butterfly.onclick = function(event) 
			{
				AddEvent(butterfly, event.target.className);	
			};		
		} 
	}
}

function AddEvent(butterfly, className)
{
	switch (className)
	{
		case "arrow_fullright":
			MoveFullRight(butterfly);
			break;						
		case "arrow_right":
			MoveSelectedRight(butterfly);	
			break;							
		case "arrow_left":
			MoveSelectedLeft(butterfly);
			break;						
		case "arrow_fullleft":
			MoveFullLeft(butterfly);
			break;									
	}
}

function MoveFullRight(butterfly)
{
	CleanErrorMessage(butterfly);

	var availableSelectbox = GetButterflyChild(butterfly, "available").getElementsByTagName("select")[0]; 
	var selectedSelectbox = GetButterflyChild(butterfly, "selected").getElementsByTagName("select")[0];
	
	var length = availableSelectbox.options.length; 

	if (length > 0)
	{
		AddAllToSelectbox(availableSelectbox, selectedSelectbox);
		CleanSelectbox(availableSelectbox);
	}
} 

function MoveSelectedRight(butterfly)
{
	CleanErrorMessage(butterfly);

	var availableSelectbox = GetButterflyChild(butterfly, "available").getElementsByTagName("select")[0]; 

	if (SelectionExists(availableSelectbox))
	{
		var options = GetSelectedOptions(availableSelectbox);
		var selectedSelectbox = GetButterflyChild(butterfly, "selected").getElementsByTagName("select")[0];

		AddOptions(options, selectedSelectbox);
		RemoveOptions(options, availableSelectbox);
		CleanErrorMessage(butterfly);
	}
	else
	{
		PrintErrorMessage(butterfly, "No options selected!");
	}
} 

function MoveSelectedLeft(butterfly)
{
	CleanErrorMessage(butterfly);

	var selectedSelectbox = GetButterflyChild(butterfly, "selected").getElementsByTagName("select")[0]; 

	if (SelectionExists(selectedSelectbox))
	{
		var options = GetSelectedOptions(selectedSelectbox);
		var availableSelectbox = GetButterflyChild(butterfly, "available").getElementsByTagName("select")[0];

		AddOptions(options, availableSelectbox);
		RemoveOptions(options, selectedSelectbox);	
		CleanErrorMessage(butterfly);
	}
	else
	{
		PrintErrorMessage(butterfly, "No options selected!");
	}
} 

function MoveFullLeft(butterfly)
{
	CleanErrorMessage(butterfly);

	var availableSelectbox = GetButterflyChild(butterfly, "available").getElementsByTagName("select")[0]; 
	var selectedSelectbox = GetButterflyChild(butterfly, "selected").getElementsByTagName("select")[0];
	
	var length = selectedSelectbox.options.length; 

	if (length > 0)
	{
		AddAllToSelectbox(selectedSelectbox, availableSelectbox);	
		CleanSelectbox(selectedSelectbox);
	}
} 

function AddAllToSelectbox(selectboxFrom, selectboxTo)
{
	AddOptions(GetAllOptions(selectboxFrom), selectboxTo);
}

function GetAllOptions(selectbox)
{
    var selectedOptions = [];

    for (var i = 0; i < selectbox.length; i++) 
	{
		var option = selectbox.options[i];
		
		selectedOptions.push(option.text);
    }

	return selectedOptions; 
}

function GetSelectedOptions(selectbox)
{
    var selectedOptions = [];

    for (var i = 0; i < selectbox.length; i++) 
	{
        var option = selectbox.options[i];

		if (option.selected) 
		{
			selectedOptions.push(option.text);
		}
    }

	return selectedOptions; 
} 

function AddOptions(options, selectbox)
{
	for (let option of options)
	{
		var newOption = document.createElement("option");
		newOption.text = option;
		
		selectbox.appendChild(newOption);
	}
}

function RemoveOptions(options, selectbox)
{
	for (var i = 0; i < selectbox.length; i++) 
	{
		var text = selectbox.options[i].text; 

		if (options.includes(text))
		{
			selectbox.remove(i);
			i--;
		}
	}
}

function CleanSelectbox(selectbox)
{
	do
	{
		selectbox.remove(0);
	}
	while (selectbox.options.length > 0);
} 

function SelectionExists(selectbox)
{
	return selectbox.selectedIndex != -1;
}

function GetButterflyChild(butterfly, className)
{
	for (let child of butterfly.children)
	{
		if (child.className == className)
		{
			return child;  
		}
	}	
}

function PrintErrorMessage(butterfly, errorText)
{
	GetButterflyChild(butterfly, "error_message").textContent = errorText;
}

function CleanErrorMessage(butterfly)
{
	GetButterflyChild(butterfly, "error_message").textContent  = "";	
}
