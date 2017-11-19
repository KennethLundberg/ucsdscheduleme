// Write your JavaScript code.
function updateMetadata(metadataList)
{

}

function insertMetadata(metadata)
{

}

function typeAhead(e) {
    console.log("working!");
    clearSearch();
    var input = e.target.value;
    var xhr = new XMLHttpRequest();
    console.log(myApp.urls.typeAhead);
    xhr.open("POST", myApp.urls.typeAhead, true);    
    xhr.send(input);

/*    xhr.onreadystatechange = function() 
    {
        if (xhr.readyState == 4 && xhr.status == 200)
        {
            clearSearch();
            for (i = 0; i < xhr.responseText.length(); i++)
            {
                populateSearch(xhr.responseText[i]);
            }
        }
    }*/ 
}

function clearSearch()
{
    var courses = document.getElementsByClassName("courseItem");
    while (courses[0])
    {
        courses[0].remove();
    }
}

function populateSearch(data)
{

}
