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
                console.log(text[i]);
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
    course.innerText = data.abbreviation;

    course.id = data.id;

    console.log("populateSearch");
    console.log(course);

    // Add it to the drop down
    courses.append(course);
}


/**
 * Adds the course selected to the class list below the search bar.
 */
function addList(data) 
{
    console.log("HELLO");
    console.log(data);

    // Hide dropdown menu
    var dropdown = document.getElementById("courseItems");
    dropdown.style.display = "none";

    // Create the element the add to the course list
    var list = document.getElementById("class-list");

    var course = document.createElement('div');
    course.className = "class";
    course.id = data.id;

    var span = document.createElement('span');
    span.innerText = data.innerText;

    console.log("data.id: " + data.id);
    myApp.coursesToSchedule.push(data.id);

    var iconContainer = document.createElement('div');
    iconContainer.className = "class-icon";
    var icon = document.createElement('i');
    icon.className = "fa fa-window-close";
    icon.setAttribute("aria-hidden", true);
    iconContainer.append(icon);

    // Add it to the course list
    course.append(span);
    course.append(iconContainer);
    list.append(course);
}