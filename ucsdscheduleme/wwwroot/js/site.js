var courses = {
    "cse101": {
        "selectedBase": "A",
        "selectedSection": "90210",
        "bases": {
            "A": {
                "baseElements": {
                    //Array of base object calenar events here (like lectures)
                },
                "sectionElements": {
                    "90210": {
                        //Array of this section specific courses here
                    },
                    "91023": {
                        //Array of this section specific courses here
                    }
                },
                "metadata": {
                    "class": "cse 110",
                    "professor": "Gary",
                    "avgWorkload": 10,
                    "avgGpaExpected": 3.5,
                    "avgGpaRecieved": 2.9
                }
            },
            "B": {
                "baseElements": [
                    "calendarEventObject1",
                    "calendarEventObject2",
                    "calendarEventObject2",
                ],
                "sectionElements": {
                    "10210": {
                        //Array of this section specific courses here
                    },
                    "11023": {
                        //Array of this section specific courses here
                    }
                },
                "metadata": {
                    //the actual object here
                }
            }
        }
    },
    "cse110": {
        //All the stuff above, but for this course
    },
    "cse190": {
        //All the stuff above, but for this course
    }
}
console.log("courses");
console.log(courses["cse101"].bases["A"].metadata);

// Write your JavaScript code.

//clear method that removes all divs for individual classes and its children.Also clearing the table of overall metadata.
function clearMetadata()
{
    //clear metadata of course-stat-container
    var courses = document.getElementById("course-stat-container");
    while (courses[0])
    {
        courses[0].parentNode.removeChild(courses[0]);
    }
}

//a function that takes in a metadata object for an individual course and adds it to the view.
function insertMetadata(course)
{ 

    // TO DO: check that metadata is properly being brought into these divs
    //outer course stat div
    var metadata = extractMetadata(course);

    var courseMetadata = document.createElement('div');
    courseMetadata.className = "course-stat";

    //class name
    var courseName = document.createElement('div');
    courseName.className = "course-stat";
    courseName.innerHTML = metadata.className;

    //professor name
    var professorName = document.createElement('div');
    professorName.className = "professor-name";
    professorName.innerHTML = metadata.professorName;

    //average work load
    var avgWorkload = document.createElement('div');
    avgWorkload.className = "class-info";
    avgWorkload.innerHTML = metadata.averageTotalWorkload;

    //average expected
    var avgExpected = document.createElement('div');
    avgExpected.className = "class-info";
    avgExpected.innerHTML = metadata.averageGpaExpected;

    //average recieved
    var avgRecieved = document.createElement('div');
    avgRecieved.className = "class-info";
    avgRecieved.innerHTML = metadata.averageGpaRecieved;

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
function updateOverallMetadata(courses)
{
    var overallWorkload;
    var overallExpectedGpa;
    var overallRecievedGpa;

    //div that holds table. table will be appended to this.
    var overallStat = document.getElementById("overall-stat");

    var tbl = document.createElement("table");
    var tblBody = document.createElement("tbody");

    //IGNORE, just an example I was trying to follow but decided a loop wasn't good for what I wanted to do

    /*for (var i = 0; i < 2; i++) {
        // creates a table row
        var row = document.createElement("tr");

        for (var j = 0; j < 2; j++) {
            // Create a <td> element and a text node, make the text
            // node the contents of the <td>, and put the <td> at
            // the end of the table row
            var cell = document.createElement("td");
            var cellText = document.createTextNode("cell in row " + i + ", column " + j);
            cell.appendChild(cellText);
            row.appendChild(cell);
        }

        // add the row to the end of the table body
        tblBody.appendChild(row);
    }*/

    //create table rows
    var workloadRow = document.createElement("tr");
    var expectedRow = document.createElement("tr");
    var recievedRow = document.createElement("tr");

    //workload row
    var workloadLabelCell = document.createElement("td");
    var workloadLabelCellText = document.createTextNode("Overall Avg. Workload");
    workloadLabelCell.appendChild(workloadLabelCellText);
    workloadRow.appendChild(workloadLabelCell);



    // put the <tbody> in the <table>
    tbl.appendChild(tblBody);
    // appends <table> into <body>
    overallStat.appendChild(tbl);
    //iterate through all the metadata in the JSON
    /*for (course in courses)
    {
        var metadata = extractMetadata(course);
        overallWorkload = overallWorkload + metadata.overallWorkload;
        overallExpectedGpa = overallExpectedGpa + metadata.averageGpaExpected;
        overallRecievedGpa = overallRecievedGpa + metadata.averageGpaRecieved;
    }*/
}

//metadata extraction helper function
function extractMetadata(course)
{

    // extract selected base - classes to update
    var selectedBase = course.selectedBase;

    // the specific class you're interested in
    var base = course.bases[selectedBase];

    // the metadata for that class
    var metadata = base.metadata;

    // the individual elements of the metadata
    var className = metadata.courseAbbreviation;
    var profName = metadata.professorName;
    var avgTotalWorkload = metadata.averageTotalWorkload;
    var avgGpaExpected = metadata.averageGpaExpected;
    var avgGpaRecieved = metadata.averageGpaRecieved;

    // return object with metadata for class
    return {
        className: className,
        professorName: profName,
        averageTotalWorkload: avgTotalWorkload,
        averageGpaExpected: avgGpaExpected,
        averageGpaRecieved: avgGpaRecieved
    };
}

        