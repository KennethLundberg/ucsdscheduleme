/** START OF DELETE **/

var bases = ["A", "B"];
var randomBaseIndex = Math.floor(Math.random() * 2);
var randomSelectionIndex = Math.floor(Math.random() * 2);

var randomBase = bases[randomBaseIndex];

var sectionsA = ["90210", "91023"];
var sectionsB = ["10201", "11023"];

var randomSelection = (randomBaseIndex == 0) ? sectionsA[randomSelectionIndex] : sectionsB[randomSelectionIndex];
var randomSelectionForOneBase = sectionsA[randomBaseIndex];

var courses = {
    "cse101": {
        "selectedBase": randomBase,
        "selectedSection": randomSelection,
        "bases": {
            "A": {
                //Array of base object calenar events here (like lectures)
                "oneTimeEvents": [
                    {
                        "courseAbbreviation": "CSE 101",
                        "date": "Monday 11/17",
                        "time": "11:00AM-3:00PM",
                        "location": "PETERSON 123",
                        "type": "FI"
                    }
                ],
                "baseElements": [
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: 30,
                        durationInMinutes: 50,
                        timespan: "8:00am - 8:50am",
                        day: "monday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: 30,
                        durationInMinutes: 50,
                        timespan: "8:00am - 8:50am",
                        day: "wednesday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: 30,
                        durationInMinutes: 50,
                        timespan: "8:00am - 8:50am",
                        day: "friday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "90210": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 101",
                            professor: "Miles, Jones",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (10 * 60 + 30),
                            durationInMinutes: 110,
                            timespan: "6:00pm - 7:50pm",
                            day: "tuesday"
                        }
                    ],
                    "91023": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 101",
                            professor: "Miles, Jones",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (11 * 60 + 30),
                            durationInMinutes: 50,
                            timespan: "7:00pm - 8:50pm",
                            day: "tuesday"
                        }
                    ]
                },
                "metadata": [
                    {
                        courseAbbreviation: "CSE 110",
                        professor: "Gary Gillespie",
                        avgWorkload: "Avg. Workload: 69 Hrs/Wk ",
                        avgGradeExpected: "Avg. Grade Expected: F- (0.00)",
                        avgGradeReceived: "Avg. Grade Received: F- (0.00)",
                    }
                ]
            },
            "B": {
                "oneTimeEvents": [
                    {
                        "courseAbbreviation": "CSE 101",
                        "date": "Friday 11/30",
                        "time": "8:00AM-12:00PM",
                        "location": "GALBRAITH 123",
                        "type": "FI"
                    },
                ],
                "baseElements": [
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: 150,
                        durationInMinutes: 50,
                        timespan: "10:00am - 10:50am",
                        day: "monday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: 150,
                        durationInMinutes: 50,
                        timespan: "10:00am - 10:50am",
                        day: "wednesday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: 150,
                        durationInMinutes: 50,
                        timespan: "10:00am - 10:50am",
                        day: "friday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "10201": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 101",
                            professor: "Miles, Jones",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (10 * 60 + 30),
                            durationInMinutes: 110,
                            timespan: "6:00pm - 7:50pm",
                            day: "thursday"
                        }
                    ],
                    "11023": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 101",
                            professor: "Miles, Jones",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (11 * 60 + 30),
                            durationInMinutes: 50,
                            timespan: "7:00pm - 8:50pm",
                            day: "monday"
                        }
                    ]
                },
                "metadata": {
                    //the actual object here
                }
            }
        }
    },
    "cse100": {
        "selectedBase": randomBase,
        "selectedSection": randomSelection,
        "bases": {
            "A": {
                //Array of base object calenar events here (like lectures)
                "oneTimeEvents": [
                    {
                        "courseAbbreviation": "CSE 100",
                        "date": "Thursday 12/6",
                        "time": "7:00PM-10:00PM",
                        "location": "WARREN LECTURE HALL 123",
                        "type": "FI"
                    },
                    {
                        "courseAbbreviation": "CSE 100",
                        "date": "Wednesday 10/12",
                        "time": "5:00PM-7:00PM",
                        "location": "YORK 123",
                        "type": "MI"
                    }
                ],
                "baseElements": [
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Alvarado, Christine",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (3 * 60 + 1 * 30),
                        durationInMinutes: 80,
                        timespan: "11:0am-12:20pm",
                        day: "tuesday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Alvarado, Christine",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (3 * 60 + 1 * 30),
                        durationInMinutes: 80,
                        timespan: "11:0am-12:20pm",
                        day: "thursday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "90210": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 100",
                            professor: "Alvarado, Christine",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (10 * 60 + 30),
                            durationInMinutes: 50,
                            timespan: "6:00pm - 6:50pm",
                            day: "friday"
                        }
                    ],
                    "91023": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 100",
                            professor: "Alvarado, Christine",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (11 * 60 + 30),
                            durationInMinutes: 50,
                            timespan: "7:00pm - 8:50pm",
                            day: "monday"
                        }
                    ]
                },
                "metadata": [

                ]
            },
            "B": {
                "oneTimeEvents": [
                    {
                        "courseAbbreviation": "CSE 100",
                        "date": "Tuesday 12/7",
                        "time": "8:00AM-12:00PM",
                        "location": "PEPPER CANYON 123",
                        "type": "FI"
                    },
                    {
                        "courseAbbreviation": "CSE 100",
                        "date": "Wednesday 10/12",
                        "time": "9:00AM-1:00PM",
                        "location": "CENTER 123",
                        "type": "MI"
                    }
                ],
                "baseElements": [
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Alvarado, Christine",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (5 * 60 + 1 * 30),
                        durationInMinutes: 80,
                        timespan: "12:00pm-1:20pm",
                        day: "tuesday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Alvarado, Christine",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (5 * 60 + 1 * 30),
                        durationInMinutes: 80,
                        timespan: "12:00pm-1:20pm",
                        day: "thursday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "10201": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 100",
                            professor: "Alvarado, Christine",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (9 * 60 + 30),
                            durationInMinutes: 50,
                            timespan: "5:00pm - 5:50pm",
                            day: "thursday"
                        }
                    ],
                    "11023": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 100",
                            professor: "Alvarado, Christine",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (8 * 60 + 30),
                            durationInMinutes: 50,
                            timespan: "4:00pm - 4:50pm",
                            day: "monday"
                        }
                    ]
                },
                "metadata": {
                    //the actual object here
                }
            }
        }
    },
    "cse110": {
        "selectedBase": "A",
        "selectedSection": randomSelectionForOneBase,
        "bases": {
            "A": {
                //Array of base object calenar events here (like lectures)
                "oneTimeEvents": [
                    {
                        "courseAbbreviation": "CSE 110",
                        "date": "Saturday 11/27",
                        "time": "10:00AM-12:00PM",
                        "location": "CENTER 420",
                        "type": "FI"
                    },
                ],
                "baseElements": [
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Gillespie, Gary",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (9 * 60 + 1 * 30),
                        durationInMinutes: 170,
                        timespan: "5:00pm-7:50pm",
                        day: "tuesday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "90210": [
                        {
                            type: "lab",
                            courseAbbreviation: "CSE 100",
                            professor: "Gillespie, Gary",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (5 * 60 + 30),
                            durationInMinutes: 170,
                            timespan: "5:00pm - 7:50pm",
                            day: "wednesday"
                        }
                    ],
                    "91023": [
                        {
                            type: "lab",
                            courseAbbreviation: "CSE 100",
                            professor: "Gillespie, Gary",
                            code: "A01",
                            startTimeInMinutesAfterFirstHour: (1 * 60 + 30),
                            durationInMinutes: 170,
                            timespan: "9:00am - 11:50am",
                            day: "monday"
                        }
                    ]
                },
                "metadata": [

                ]
            }
        }
    }

}

/**
 * test adding calendar events, will delete later
 **/
function setup() {
    clearMeetings();
    clearOneTimeEvents();
    console.log("setup()");
    updateMeetings(courses);
    //updateOneTimeEvents(courses);
    clearMetadata();
}
/** END OF DELETE **/

/* called when DOM is ready */
document.addEventListener('DOMContentLoaded', function() {
    setup();
});

function typeAheadCallout(input) {
    var xhr = new XMLHttpRequest();
    var url = myApp.urls.typeAhead;
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    var send = { "input": input, "alreadyAddedCourses": myApp.coursesToSchedule };
    console.log("Payload: " + JSON.stringify(send));
    xhr.send(JSON.stringify(send));

    // When the text is edited, it clears the search and populates it
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var text = JSON.parse(xhr.responseText);
            clearSearch();

            for (i = 0; i < text.length; i++) {
                populateSearch(text[i]);
                console.log(text[i]);
            }
        }
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
        courses.removeChild(courses[0]);
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

/**
 * Function: insertMeeting(meeting)
 * Param: meeting - the meeting to insert
 * Description: Inserts a single meeting into the calendar.
 *  A meeting has the following structure
    <div class="event">
        <div class="edit-button"><span>Change</span><i class="fa fa-cog" aria-hidden="true"></i></div>
        <div class="unlock-button"><i class="fa fa-unlock" aria-hidden="true"></i></div>
        <div class="event-header">
            <div class="icon" id="lecture">LE</div>
            <div class="event-info">
                <span>CSE 110</span>
                <span>Gary Gillespie</span>
                <span>8:00am - 9:20am</span>
                <span>A00</span>
            </div>
        </div>
    </div>
 * Creates each div and create the div hiearchy as shown above. Create and add the spans to the "event-info"
 * div. Populate divs and spans with data from the parameter. Use calculateMeetingPosition function to size
 * and place the event div.
 *  Each div is assigned the appropriate class and id.
 */
function insertMeeting(meeting)
{
    /* Calculate the meeting position using helper function */
    var pos = calculateMeetingPosition(meeting);
    var top = pos.top;
    var height = pos.height;
    
    /* create an event div */
    var event = document.createElement('div');
    event.style.top = top;
    event.className = "event";
    event.style.height = height;

    /* create an event header div and add to event div */
    var eventHeader = document.createElement('div');
    eventHeader.className = "event-header";
    event.append(eventHeader);

    /* create an icon and add to event header div */
    var icon = document.createElement('div');
    icon.className = "class-icon";
    icon.id = meeting.type;
    eventHeader.append(icon);


    /* create the Change and edit-button icon and add to event div */
    var editButtonContainer = document.createElement('div');
    editButtonContainer.className = "edit-button";
    event.append(editButtonContainer);

    var editButtonText = document.createElement('span');
    editButtonText.innerText = "Change";
    editButtonContainer.append(editButtonText);

    var editButtonIcon = document.createElement('i');
    editButtonIcon.className = "edit-button fa fa-cog";
    editButtonContainer.append(editButtonIcon);

    /* create the Change and edit-button icon and add to event div */
    var unlockButtonContainer = document.createElement('div');
    unlockButtonContainer.className = "unlock-button";
    event.append(unlockButtonContainer);

    var unlockButtonIcon = document.createElement('i');
    unlockButtonIcon.className = "edit-button fa fa-unlock";
    unlockButtonContainer.append(unlockButtonIcon);

    /* create an icon label and add to icon div */
    var iconLabel = document.createElement('div');
    iconLabel.className = "class-icon-label";
    iconLabel.innerText = "LE";
    icon.append(iconLabel);

    // Use first two letters as text
    iconLabel.innerText = meeting.type.toUpperCase().substr(0,2);

    /* create an event info div */
    var eventInfo = document.createElement('div');
    eventInfo.className = "event-info";

    /** create spans from meeting object **/
    /* courseAbbreviation */
    var classSpan = document.createElement('span');
    classSpan.innerHTML = meeting.courseAbbreviation;

    /* professor */
    var profSpan = document.createElement('span');
    profSpan.innerHTML = meeting.professor;

    /* time range */
    var timeSpan = document.createElement('span');
    timeSpan.innerHTML = meeting.timespan;

    /* section code */
    var sectSpan = document.createElement('span');
    sectSpan.innerHTML = meeting.code;

    /* add spans to event info div */
    eventInfo.append(classSpan);
    eventInfo.append(profSpan);
    eventInfo.append(timeSpan);
    eventInfo.append(sectSpan);
    eventHeader.append(eventInfo);

    /* add event to day of meeting */
    var dayElem = document.getElementById(meeting.day);
    dayElem.append(event);
}

/**
 * Function: updateMeetings(meetings)
 * Param: meetings - the JSON object with a list of selected bases, selections
 *      See global variable TODO for the structure
 * Description: From the list of all bases and sections, get only the selected ones.
 *      Then add each event to the calendar by calling insertMeeting on each meeting
 */
function updateMeetings(meetings)
{
    /* iterate through all the meetings in the JSON */
    for(meeting in meetings) {

        /* extract selected base and section - the events to display on calendar */
        var selectedBase = courses[meeting].selectedBase;
        var selectedSection = courses[meeting].selectedSection;

        /* get list of selected base (i.e. lectures) and section elements (i.e. discussions) */
        var baseElements = courses[meeting].bases[selectedBase].baseElements;
        var sectionElements = courses[meeting].bases[selectedBase].sectionElements[selectedSection];

        /* insert all base elements */
        for(var i = 0; i < baseElements.length; i++) {
            insertMeeting(baseElements[i]);
        }

        /* check if there are any sections */
        if(sectionElements != null) {

            /* insert all section elements */
            for(var i = 0; i < sectionElements.length; i++) {
                insertMeeting(sectionElements[i]);
            }
        }
    }
}

/**
 * @description Clears the current table of one time events
 */
function clearOneTimeEvents()
{
    var oneTimeEvents = document.getElementById('onetime');

    while (oneTimeEvents.firstChild)
    {
        oneTimeEvents.removeChild(oneTimeEvents.firstChild);
    } 
}

/**
 * @description Insersts the one time event data into the view
 * @param {OneTimeEvent} oneTimeEventData The current one time event object
 */
function insertOneTimeEvents(oneTimeEventData)
{
    var oneTimeEvent = document.createElement('tr');

    var courseAbbrev = document.createElement('td');
    courseAbbrev.innerHTML = oneTimeEventData.courseAbbreviation;

    var date = document.createElement('td');
    date.innerHTML = oneTimeEventData.date;

    var time = document.createElement('td');
    time.innerHTML = oneTimeEventData.time;

    var location = document.createElement('td');
    location.innerHTML = oneTimeEventData.location;

    var type = document.createElement('td');
    type.innerHTML = oneTimeEventData.type;

    oneTimeEvent.append(courseAbbrev);
    oneTimeEvent.append(type);
    oneTimeEvent.append(date);
    oneTimeEvent.append(time);
    oneTimeEvent.append(location);

    var oneTimeEventTable = document.getElementById('onetime');
    oneTimeEventTable.append(oneTimeEvent);
}

/**
 * @description Updates the one time event table to hold have the current schedule
 * @param {ScheduleViewModel} courses Dictionary of CourseViewModels
 */
function updateOneTimeEvents(courses)
{
    /* iterate through all the meetings in the JSON */
    for (course in courses) {

        /* extract selected base - the events to display on calendar */
        var selectedBase = courses[course].selectedBase;

        /* get list of one time events (i.e. finals) */
        var oneTimeEvents = courses[course].bases[selectedBase].oneTimeEvents;

        /* insert all one time events */
        for (var i = 0; i < oneTimeEvents.length; i++)
        {
            insertOneTimeEvents(oneTimeEvents[i]);
        }
    }
}

