// Write your JavaScript code.

//clear method that removes all divs for individual classes and its children.Also clearing the table of overall metadata.
function clearMetadata()
{
    //clear metadata of course-stat-container
    var courses = document.getElementById("course-stat-container");
    while (courses.firstChild)
    {
        courses.removeChild(courses.firstChild);
    }
    
}

//a function that takes in a metadata object for an individual course and adds it to the view.
function insertMetadata(metadata)
{
    //outer course stat div
    var courseMetadata = document.createElement('div');
    courseMetadata.className = "course-stat";

    //class name
    var courseName = document.createElement('div');
    courseName.className = "course-stat";
    //professor name
    var professorName = document.createElement('div');
    professorName.className = "professor-name";
    //average work load
    var avgWorkload = document.createElement('div');
    avgWorkload.className = "class-info";
    //average expected
    var avgExpected = document.createElement('div');
    avgExpected.className = "class-info";
    //average recieved
    var avgRecieved = document.createElement('div');
    avgRecieved.className = "class-info";

    //attaching all class information to course div
    courseMetadata.append(courseName);
    courseMetadata.append(professorName);
    courseMetadata.append(avgWorkload);
    courseMetadata.append(avgExpected);
    courseMetadata.append(avgRecieved);

    //retrieving course-stat-container
    var courseGoesHere = document.getElementById("course-stat-container");

    //insert course container to right side bar
    courseGoesHere.append(courseMetadata);
}

//a function that updates the metadata by calling the InsertMetadata function for each course metadata
function updateMetadata(metadataList)
{
    for (var i = 0; i < metadataList.length; i++)
    {
        insertMetadata(metadataList[i]);
    }
    
}

// a function that updates the overall metadata table in the view by iterating through the list of metadata and calculating the new overall data
function updateOverallMetadata()
{

}