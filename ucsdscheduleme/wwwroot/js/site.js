/** START OF DELETE **/
/* Randomly select from demo data - TODO Delete later! */
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
                            startTimeInMinutesAfterFirstHour: (10*60+30),
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
                            startTimeInMinutesAfterFirstHour: (11*60+30),
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
                            startTimeInMinutesAfterFirstHour: (10*60+30),
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
                            startTimeInMinutesAfterFirstHour: (11*60+30),
                            durationInMinutes: 50,
                            timespan: "7:00pm - 8:50pm",
                            day: "monday"
                        }
                    ]
                },
                "metadata" : {
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
                "baseElements": [
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Alvarado, Christine",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (3*60+1*30),
                        durationInMinutes: 80,
                        timespan: "11:0am-12:20pm",
                        day: "tuesday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Alvarado, Christine",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (3*60+1*30),
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
                            startTimeInMinutesAfterFirstHour: (10*60+30),
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
                            startTimeInMinutesAfterFirstHour: (11*60+30),
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
                "baseElements": [
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Alvarado, Christine",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (5*60+1*30),
                        durationInMinutes: 80,
                        timespan: "12:00pm-1:20pm",
                        day: "tuesday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Alvarado, Christine",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (5*60+1*30),
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
                            startTimeInMinutesAfterFirstHour: (9*60+30),
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
                            startTimeInMinutesAfterFirstHour: (8*60+30),
                            durationInMinutes: 50,
                            timespan: "4:00pm - 4:50pm",
                            day: "monday"
                        }
                    ]
                },
                "metadata" : {
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
                "baseElements": [
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 100",
                        professor: "Gillespie, Gary",
                        code: "A00",
                        startTimeInMinutesAfterFirstHour: (9*60+1*30),
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
                            startTimeInMinutesAfterFirstHour: (5*60+30),
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
                            startTimeInMinutesAfterFirstHour: (1*60+30),
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
    console.log("setup()");
    updateMeetings(courses);
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

function removeCourse(e) {
    var course = e.target.parentNode.parentNode;
    var id = course.id;
    var index = myApp.coursesToSchedule.indexOf(id);
    myApp.coursesToSchedule.splice(index, 1);
    console.log("Courses: " + myApp.coursesToSchedule);
    course.remove();
    typeAheadCallout(document.getElementById("search").value);
}

/**
 * Clears the drop down and populates the drop down with the auto-complete results.
 * @param {String} e The string to search for auto-complete results with.
 */
function typeAhead(e) {

    // Sends the input to the server to get the courses
    var input = e.target.value;
    typeAheadCallout(input);
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

    // Now clear the dropdown and repopulate it
    typeAheadCallout(document.getElementById("search").value);

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

/**
 * Function: clearMeetings()
 * Param: none
 * Description: Clears the calendar of events by removing all elements with class 'event'
 */
function clearMeetings()
{
    /* retrieve elements with class 'event' */
    var elements = document.getElementsByClassName('event');

    /* remove first element in resulting list until all children are deleted*/
    while(elements[0]) {
        elements[0].parentNode.removeChild(elements[0]);
    }
}

/**
 * Function: calculateMeetingPosition(meeting)
 * Param: meeting - the meeting to insert
 * Description: Based on the time and duration of the event, calculate the top and height of the event element.
 *      This can be done because the height of any 30 minute increment is fixed, so a calculation of the top is just
 *      (# half hour increments after 7:30 am) * (height of individual 30 min increment) in px.
 *      Then height is just (duration of event in minutes) * (height of 30 min section) / 30.
 * Return: returns the top and height values
 */
function calculateMeetingPosition(meeting) {

    /* calculate top and height based on number of half hour increments after 7:30am and duration */
    var numHalfHourInc = (meeting.startTimeInMinutesAfterFirstHour) / 30;
    var height30MinInc = document.querySelector(".time").childNodes[1].offsetHeight;
    var timeOffSet = 50;

    var top = (numHalfHourInc * height30MinInc + timeOffSet) + "px";
    var height = ((meeting.durationInMinutes) * (height30MinInc) / 30) + "px";

    return {
        top: top,
        height: height
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


            /* insert all section elements */
            for(var i = 0; i < sectionElements.length; i++) {
                insertMeeting(sectionElements[i]);
            }
        }
    }
}

//clear method that removes all divs for individual classes and its children.Also clearing the table of overall metadata.
function clearMetadata() {
    //clear metadata of course-stat-container
    var courses = document.getElementById("course-stat-container");
    while (courses.firstChild) {
        courses.removeChild(courses.firstChild);
    }

}

//a function that takes in a metadata object for an individual course and adds it to the view.
function insertMetadata(metadata) {
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
function updateMetadata(metadataList) {
    for (var i = 0; i < metadataList.length; i++) {
        insertMetadata(metadataList[i]);
    }

}

// a function that updates the overall metadata table in the view by iterating through the list of metadata and calculating the new overall data
function updateOverallMetadata() {
}