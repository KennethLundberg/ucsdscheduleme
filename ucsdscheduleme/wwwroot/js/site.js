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
    console.log(myApp.urls.typeAhead);
    xhr.open("POST", myApp.urls.typeAhead, true);    
    xhr.send({"input" : input });

    // When the text is edited, it clears the search and populates it
    xhr.onreadystatechange = function() 
    {
        if (xhr.readyState == 4 && xhr.status == 200)
        {
            clearSearch();
            for (i = 0; i < (xhr.responseText.length - 2); i++)
            {
                populateSearch(xhr.responseText[i]);
            }
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
    course.innerText = data;

    // Add it to the drop down
    courses.add(course);
    course.addEventListener("onclick", addList);
}


/**
 * Adds the course selected to the class list below the search bar.
 */
function addList() 
{
    // Create the element the add to the course list
    var list = document.getElementById("class-list");
    var course = document.createElement('div');
    var span = document.createElement('span');
    span.innerText = this.name;
    var icon = document.createElement('div');
    icon.className = "class-icon";

    // Add it to the course list
    course.append(span);
    course.append(icon);
    list.append(course);
}