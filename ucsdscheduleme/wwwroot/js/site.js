// Write your JavaScript code.
function updateMetadata(metadataList)
{

}

function insertMetadata(metadata)
{

}


/**
 * Clears the drop down and populates the drop down with the auto-complete results.
 * @param {String} e The string to search for auto-complete results with.
 */
function typeAhead(e) {
    clearSearch();

    // Sends the input to the server to get the courses
    var input = e.target.value;
    var xhr = new XMLHttpRequest();
    var safeInput = encodeURI(input);
    var url = myApp.urls.typeAhead + "?input=" + safeInput;
    xhr.open("GET", url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');

    xhr.send();

    // When the text is edited, it clears the search and populates it
    xhr.onreadystatechange = function() 
    {
        if (xhr.readyState == 4 && xhr.status == 200)
        {
            var text = JSON.parse(xhr.responseText);
            clearSearch();
            for (i = 0; i < text.length; i++)
            {
                populateSearch(text[i]);
            }
/*            var list = document.getElementsByClassName("courseItem");
            var listLength = list.length;
            for (i = 0; i < listLength; i++)
            {
                console.log(list[i]); 
                //list[i].addEventListener("mouseup", addList());
                list[i].onclick = addList(); 
            } */
        }
    }
}


/**
 * Clears the courses from the drop down.
 */
function clearSearch()
{
    var courses = document.getElementsByClassName("courseItem");
    while (courses[0])
    {
        courses[0].remove();
    }
}


/**
 * Populates the search drop down with the auto-complete results.
 * @param {Number} data the data to populate the drop down with.
 */
function populateSearch(data)
{
    // Create the element to populate the search with
    var courses = document.getElementById("courseItems");
    var course = document.createElement('div');
    course.className = "courseItem";
    course.innerText = data.abbreviation;

    // Add it to the drop down
    courses.append(course);
}


/**
 * Adds the course selected to the class list below the search bar.
 */
function addList(e) 
{
    console.log("HELLO");
    // Create the element the add to the course list
    var list = document.getElementById("class-list");
    var course = document.createElement('div');
    var span = document.createElement('span');
    span.innerText = e.name;
    var icon = document.createElement('div');
    icon.className = "class-icon";

    // Add it to the course list
    course.append(span);
    course.append(icon);
    list.append(course);
}