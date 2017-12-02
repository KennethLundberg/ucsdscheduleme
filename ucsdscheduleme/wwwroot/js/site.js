/** START OF DELETE **/

var bases = ["A", "B"];
var randomBaseIndex = Math.floor(Math.random() * 2);
var randomSelectionIndex = Math.floor(Math.random() * 2);

var randomBase = bases[randomBaseIndex];

var sectionsA = ["90210", "91023"];
var sectionsB = ["10201", "11023"];

var randomSelection = (randomBaseIndex == 0) ? sectionsA[randomSelectionIndex] : sectionsB[randomSelectionIndex];
var randomSelectionForOneBase = sectionsA[randomBaseIndex];

var testCourses = {
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
                "baseEvents": [
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
                "sectionEvents": {
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
                        professorName: "Gary Gillespie",
                        averageWorkload: "10.0",
                        averageGpaExpected: "3.0",
                        averageGpaReceived: "2.0",
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
                "baseEvents": [
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
                "sectionEvents": {
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
                "baseEvents": [
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
                "sectionEvents": {
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
                "baseEvents": [
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
                "sectionEvents": {
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
                "baseEvents": [
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
                "sectionEvents": {
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
    clearAllMeetings();   
    clearOneTimeEvents();
    clearMetadata();
    console.log("setup()");
    //updateMeetings(testCourses);
    //updateOneTimeEvents(testCourses);
    //updateOverallMetadata(testCourses);
}
 
/* called when DOM is ready */
document.addEventListener('DOMContentLoaded', function () {
    setup();
});


/** END OF DELETE **/



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
function clearSearch() {
    var courses = document.getElementsByClassName("courseItem");
    while (courses[0]) {
        courses[0].remove();
    }
}

/**
 * Populates the search drop down with the auto-complete results.
 * @param {Number} data the data to populate the drop down with.
 */
function populateSearch(data) {
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
function addList(data) {
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
 * Function: clearAllMeetings()
 * Param: none
 * Description: Clears the calendar of events by removing all elements with class 'event'
 */
function clearAllMeetings()
{
    /* retrieve elements with class 'event' */
    var elements = document.getElementsByClassName('event');

    /* remove first element in resulting list until all children are deleted*/
    while (elements[0]) {
        elements[0].parentNode.removeChild(elements[0]);
    }
}
function clearMeetings(className)
{
    /* retrieve elements with class 'event' */
    var elements = document.getElementsByClassName(className);

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
function insertMeeting(meeting, courseId, baseId, sectionId)
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
    icon.id = meeting.type.toLowerCase();
    eventHeader.append(icon);

    /* create the Change and edit-button icon and add to event div */
    var editButton = document.createElement('div');
    editButton.className = "edit-button";
    event.append(editButton);

    var editSpan = document.createElement('span');
    editSpan.innerHTML = "Change";
    editSpan.className = "edit-span";
    editButton.append(editSpan);

    var editIcon = document.createElement('i');
    editIcon.className = "fa fa-cog";
    editIcon.setAttribute("aria-hidden", true); 
    editButton.append(editIcon);

    event.append(editButton);

    /* create an icon label and add to icon div */
    var iconLabel = document.createElement('div');
    iconLabel.className = "class-icon-label";
    iconLabel.innerText = "LE";
    icon.append(iconLabel);

    // Use first two letters as text
    iconLabel.innerText = meeting.type.toUpperCase().substr(0, 2);
    console.log("----------");
    console.log("Meeting type: " + meeting.type);
    console.log("----------");

    /* create an event info div */
    var eventInfo = document.createElement('div');
    eventInfo.className = "event-info";

    /** create spans from meeting object **/
    /* courseAbbreviation */
    var classSpan = document.createElement('span');
    classSpan.innerHTML = meeting.courseAbbreviation;

    /* professor */
    var profSpan = document.createElement('span');
    profSpan.innerHTML = meeting.professorName;

    /* time range */
    var timeSpan = document.createElement('span');
    timeSpan.innerHTML = meeting.timespan;

    /* section code */
    var sectSpan = document.createElement('span');
    sectSpan.innerHTML = meeting.sectionCode;

    /* add spans to event info div */
    eventInfo.append(classSpan);
    eventInfo.append(profSpan);
    eventInfo.append(timeSpan);
    eventInfo.append(sectSpan);
    eventHeader.append(eventInfo);

    courseId = ' _' + courseId;
    baseId = ' _' + baseId;
    sectionId = ' _' + sectionId;
    event.className += courseId;
    event.className += baseId;
    event.className += sectionId;

    /* add event to day of meeting */
    var dayElem = document.getElementById(meeting.day.toLowerCase());
    dayElem.append(event);

    return event;
}

/**
 * Function: updateMeetings(meetings)
 * Param: meetings - the JSON object with a list of selected bases, selections
 *      See global variable TODO for the structure
 * Description: From the list of all bases and sections, get only the selected ones.
 *      Then add each event to the calendar by calling insertMeeting on each meeting
 */
function updateMeetings(meetings) {
    /* iterate through all the meetings in the JSON */
    for(meeting in meetings) {
        /* extract selected base and section - the events to display on calendar */
        var selectedBase = meetings[meeting].selectedBase;
        var selectedSection = meetings[meeting].selectedSection;

        /* get list of selected bases (i.e. lectures) and section elements (i.e. discussions) */
        var baseEvents = meetings[meeting].bases[selectedBase].baseEvents;
        var sectionEvents = meetings[meeting].bases[selectedBase].sectionEvents[selectedSection];

        /* insert all base elements */
        for(var i = 0; i < baseEvents.length; i++) {
            insertMeeting(baseEvents[i], meeting, selectedBase);
        }

        /* check if there are any sections */
        if (sectionEvents != null) {

            /* insert all section elements */
            for(var i = 0; i < sectionEvents.length; i++) {
                insertMeeting(sectionEvents[i], meeting, selectedBase, selectedSection);
            }
        }
    }
}

function showAllBasesAndAllSections(ids) {

    var course = myApp.courses[ids.courseId];
    var bases = course.bases;

    var baseKeys = Object.keys(bases);

    clearMeetings(ids.courseId);

    for(var i = 0; i < baseKeys.length; i++) {
        // insert all bases aka lectures
        for(var j = 0; j < bases[baseKeys[i]].baseEvents.length; j++) {
            var meeting = bases[baseKeys[i]].baseEvents[j];

            // insert base and set activated class
            var event = insertMeeting(meeting, ids.courseId, baseKeys[i], "undefined");
            event.className += " event-activated";
        }

        // insert each section
        var sectionEvents = bases[baseKeys[i]].sectionEvents;
        var sectionsKeys = Object.keys(sectionEvents);
        sectionsKeys.forEach(function(key) {
            var section = sectionEvents[key];

            // insert section and set activated class
            for(var k = 0; k < section.length; k++) {
                var event = insertMeeting(section[k], ids.courseId, baseKeys[i], key);
                event.className += " event-activated";
            }
        });
    }
}

function showBaseAndAllSections(ids) {
    var course = myApp.courses[ids.courseId];
    var baseEvents = course.bases[ids.baseId].baseEvents;
    var sectionEvents = course.bases[ids.baseId].sectionEvents;
    
    clearMeetings(ids.courseId);

    for(var i = 0; i < baseEvents.length; i++) {
        var event = insertMeeting(baseEvents[i], ids.courseId, ids.baseId, "undefined");
        event.className += " event-activated";
    }

    var sectionsKeys = Object.keys(sectionEvents);
    sectionsKeys.forEach(function(key) {
        var section = sectionEvents[key];
        var event = insertMeeting(section[0], ids.courseId, ids.baseId, key);
        event.className += " event-activated";
    });

    hideEditButtons();
}

function changeSchedule(event) {
   var ids = extractIds(event);

    // base selected
    if(ids.sectionId === "undefined") {
        showAllBasesAndAllSections(ids);
    } 
    // section selected
    else {
        showBaseAndAllSections(ids);
    }
}
/**
 * updateSelectedSection
 * @param: sectionId: string
 */
function updateSelectedSection(event) {
    var ids = extractIds(event);

    // update the base and section IDs in the global object
    myApp.courses[ids.courseId].selectedSection = ids.sectionId;
    myApp.courses[ids.courseId].selectedBase = ids.baseId;

    clearAllMeetings();
    updateMeetings(myApp.courses);

    isEditing = false;
    showEditButtons();
}

/**
 * @param: baseId: string, sectionId: string
 */
function updateSelectedBase(event) {
    var ids = extractIds(event);

    // update the base ID in the global object
    myApp.courses[ids.courseId].selectedBase = ids.baseId;

    clearAllMeetings();
    updateMeetings(myApp.courses)
    
    hideEditButtons();

    // showBaseAndAllSections(ids);
    var baseEvents = myApp.courses[ids.courseId].bases[ids.baseId].baseEvents;
    var sectionEvents = myApp.courses[ids.courseId].bases[ids.baseId].sectionEvents;

    showBaseAndAllSections(ids);
}

// clicked on section or base, know because bases have "_undefined" as sectionId
function updateEvent(event) {

    if(!event.classList.contains("_undefined")) {
        // selected a section
        updateSelectedSection(event);
    } else {
        // selected a base
        updateSelectedBase(event)
    }
}

function activateSelectedBasesAndSections(event) {
    var ids = extractIds(event, false);

    var allActivatedEvents = document.getElementsByClassName('event-activated');

    if(ids.sectionId !== "_undefined") {
        var toDeactivate = [];
        for(var j = 0; j < allActivatedEvents.length; j++) {
            var classList = allActivatedEvents[j].classList;

            if(classList.contains(ids.courseId) && classList.contains(ids.baseId)) {
                if(classList.contains(ids.sectionId) || classList.contains("_undefined")) {
                } else {
                    toDeactivate.push(allActivatedEvents[j]);
                }
            } else {
                toDeactivate.push(allActivatedEvents[j]);
            }
        }

        for(var k = 0; k < toDeactivate.length; k++) {
            toDeactivate[k].classList.add('event-deactivated');
            toDeactivate[k].classList.remove('event-activated');
        }

    } else {
        var toDeactivate = [];

        for(var j = 0; j < allActivatedEvents.length; j++) {
            var classList = allActivatedEvents[j].classList;

            if(!classList.contains(ids.courseId) || !classList.contains(ids.baseId)) {
                toDeactivate.push(allActivatedEvents[j]);   
            }
        }

        for(var k = 0; k < toDeactivate.length; k++) {
            toDeactivate[k].classList.add('event-deactivated');
            toDeactivate[k].classList.remove('event-activated');
        }
    }
}

function reactivateAllBasesAndAllSections(event) {
    var ids = extractIds(event, false);
    var allActivatedEvents = document.getElementsByClassName('event-deactivated');
    while(allActivatedEvents.length > 0) {
        allActivatedEvents[0].className += ' event-activated';
        allActivatedEvents[0].classList.remove('event-deactivated');
    }
}

// TODO: Replace comments
// default: returns no underscores
function extractIds(event, noUnderscore = true) {

    if(event.classList.length <= 0) {
        return null;
    }
    var classListLength = event.classList.length;

    var courseId = event.classList[1];
    var baseId = event.classList[2];
    var sectionId = event.classList[3];

    if(noUnderscore) {
        courseId = event.classList[1].substr(1);
        baseId = event.classList[2].substr(1);
        sectionId = event.classList[3].substr(1);
    }

    return {
        courseId: courseId,
        baseId: baseId,
        sectionId: sectionId
    }
}

/**
 * @param: element: DOM element of which you want to find the event div for 
 */
function findOuterDiv(element, className) {

    if(typeof element == undefined) {
        return null;
    }

    while (element && element.classList && !element.classList.contains(className)) {
        element = element.parentNode;
    }

    if(element && element.classList && element.classList.contains(className)) { 
        return element;
    }

    return null;
}

function hideEditButtons() {
    var buttons = document.getElementsByClassName('edit-button');
    for(var i = 0; i < buttons.length; i++) {
        buttons[i].style.visibility = 'hidden';
    }
}

function showEditButtons() {
    var buttons = document.getElementsByClassName('edit-button');
    for(var i = 0; i < buttons.length; i++) {
        buttons[i].style.visibility = 'visible';
    }
}

/**
 * @description clear method that removes all divs for individual classes and its children.Also clearing the table of overall metadata.
 */
function clearMetadata() {
    //clear metadata of course-stat-container
    var courses = document.getElementById("course-stat-container");
    while (courses.firstChild) {
        courses.removeChild(courses.firstChild);
    }
    clearOverallMetadata();
}

/**
 * @description clears the overall metadata table seting the values to 0.
 */
function clearOverallMetadata() {
    var workload = document.getElementById("overall-workload");
    var expected = document.getElementById("gpa-expected");
    var received = document.getElementById("gpa-received");

    workload.innerHTML = "0";
    expected.innerHTML = "0";
    received.innerHTML = "0";
}

/**
 * @description a function that takes in a metadata object for an individual course and adds it to the view.
 * @param {any} metadata
 */
function insertMetadata(metadata) {
    //outer course stat div
    var courseMetadata = document.createElement('div');
    courseMetadata.className = "course-stat";

    //class name
    var courseName = document.createElement('div');
    courseName.className = "course-name";
    courseName.innerHTML = metadata.courseAbbreviation;

    //professor name
    var professorName = document.createElement('div');
    professorName.className = "professor-name";
    professorName.innerHTML = metadata.professorName;

    var rateMyProfessor = document.createElement('div');
    rateMyProfessor.className = "course-info";
    rateMyProfessor.innerHTML = "Quality: " + metadata.quality + " Difficulty: " + metadata.difficulty;

    //average work load
    var avgWorkload = document.createElement('div');
    avgWorkload.className = "course-info";
    avgWorkload.innerHTML = "Avg.Workload: " + metadata.averageWorkload + " Hrs/Wk ";

    //average expected
    var avgExpected = document.createElement('div');
    avgExpected.className = "course-info";
    avgExpected.innerHTML = "Avg. Grade Expected: " + convertGPAToStringFormat(metadata.averageGpaExpected);

    //average recieved
    var avgRecieved = document.createElement('div');
    avgRecieved.className = "course-info";
    avgRecieved.innerHTML = "Avg. Grade Received: " + convertGPAToStringFormat(metadata.averageGpaReceived);

    //attaching all class information to course div
    courseMetadata.append(courseName);
    courseMetadata.append(professorName);
    courseMetadata.append(rateMyProfessor);
    courseMetadata.append(avgWorkload);
    courseMetadata.append(avgExpected);
    courseMetadata.append(avgRecieved);

    //retrieving course-stat-container
    var courseGoesHere = document.getElementById("course-stat-container");

    //insert course container to right side bar
    courseGoesHere.append(courseMetadata);
}

/**
 * @description a function that updates the metadata by calling the InsertMetadata function for each course metadata
 * @param {any} courses
 */
function updateMetadata(courses) {
    /* iterate through all the meetings in the JSON */
    for (courseId in courses) {
        /* extract selected base - the events to display on calendar */
        var selectedBase = courses[courseId].selectedBase;

        /* get list of one time events (i.e. finals) */
        var metadata = courses[courseId].bases[selectedBase].metadata;

        /* insert metadata */
        insertMetadata(metadata);
    }
}

/**
 * @description a function that updates the overall metadata table in the view by iterating through the list of metadata and calculating the new overall data
 * @param {any} courses
 */
function updateOverallMetadata(courses) {
    var numCourses = 0;
    var overallWorkload = 0;
    var overallExpectedGpa = 0;
    var overallReceivedGpa= 0;

    //iterate through all the metadata in the JSON
    for (course in courses)
    {
        /* extract selected base - the events to display on calendar */
        var selectedBase = courses[course].selectedBase;

        /* get list of one time events (i.e. finals) */
        var metadata = courses[course].bases[selectedBase].metadata;

        //var metadata = extractMetadata(course);
        overallWorkload = overallWorkload + metadata.averageWorkload;
        overallExpectedGpa = overallExpectedGpa + metadata.averageGpaExpected;
        overallReceivedGpa = overallReceivedGpa + metadata.averageGpaReceived;

        numCourses++;
    }

    //calculating averages
    overallWorkload /= numCourses;
    overallExpectedGpa /= numCourses;
    overallReceivedGpa /= numCourses;

    document.getElementById("overall-workload").innerHTML = convertGPAToStringFormat(overallWorkload.toFixed(2));
    document.getElementById("gpa-expected").innerHTML = convertGPAToStringFormat(overallExpectedGpa.toFixed(2));
    document.getElementById("gpa-received").innerHTML = convertGPAToStringFormat(overallReceivedGpa.toFixed(2));
}

/**
 * @description helper function that takes GPA decimal and returns equivalent letter grade in proper format.
 * @param {any} grade
 */
function convertGPAToStringFormat(grade)
{
    var prefix = "";

    if (grade >= 4.0)
    {
        prefix = "A";
    }
    else if (grade >= 3.7)
    {
        prefix = "A-";
    }
    else if (grade >= 3.3)
    {
        prefix = "B+";
    }
    else if (grade >= 3.0)
    {
        prefix = "B";
    }
    else if (grade >= 2.7)
    {
        prefix = "B-";
    }
    else if (grade >= 2.3)
    {
        prefix = "C+";
    }
    else if (grade >= 2.0)
    {
        prefix = "C";
    }
    else if (grade >= 1.7)
    {
        prefix = "C-";
    }
    else if (grade >= 1.0)
    {
        prefix = "D";
    }
    else
    {
        prefix = "F";
    }
    //format that looks like "B+ (3.82)"
    return prefix + " (" + grade + ")";
}

/**
 * @description Clears the current table of one time events
 */
function clearOneTimeEvents() {
    var oneTimeEvents = document.getElementById('onetime');

    while (oneTimeEvents.firstChild) {
        oneTimeEvents.removeChild(oneTimeEvents.firstChild);
    }
}

/**
 * @description Insersts the one time event data into the view
 * @param {OneTimeEvent} oneTimeEventData The current one time event object
 */
function insertOneTimeEvents(oneTimeEventData) {
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
function updateOneTimeEvents(courses) {
    /* iterate through all the meetings in the JSON */
    for (courseId in courses) {
        /* extract selected base - the events to display on calendar */
        var selectedBase = courses[courseId].selectedBase;

        /* get list of one time events (i.e. finals) */
        var oneTimeEvents = courses[courseId].bases[selectedBase].oneTimeEvents;

        /* insert all one time events */
        for (var i = 0; i < oneTimeEvents.length; i++) {
            insertOneTimeEvents(oneTimeEvents[i]);
        }
    }
}

function updateSchedule(courses) {
    console.log("------------------------------------------")
    console.log(JSON.stringify(courses));
    console.log("------------------------------------------")
    myApp.courses = courses;
    clearOneTimeEvents();
    updateOneTimeEvents(courses);
    clearMeetings();
    updateMeetings(courses);
    clearMetadata();
    updateOverallMetadata(courses);
    updateMetadata(courses);
}

function generateSchedule() {
    var xhr = new XMLHttpRequest();
    var url = myApp.urls.generateSchedule;
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');

    // TODO check if optimization isn't -1 and also that there is at least one course.

    // Grab optimization from select list.
    var optimizationSelect = document.getElementById("optimization");
    var selectedValue = optimizationSelect.options[optimizationSelect.selectedIndex].value;

    if (selectedValue == -1) {
        return;
        // TODO Error Message
    }

    // Grab courses to schedule
    var courseIds = myApp.coursesToSchedule;
    if (courseIds.length < 1) {
        return;
        // TODO error message
    }

    var request = { "optimization": selectedValue, "courseIds": courseIds };

    console.log("Payload: " + JSON.stringify(request));

    xhr.send(JSON.stringify(request));

    // When the text is edited, it clears the search and populates it
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var response = JSON.parse(xhr.responseText);
            updateSchedule(response.courses);
        }
    }
}

function visibility_on(id) {
    var e = document.getElementById(id);
    e.style.display = 'block';
}

function visibility_off(id) {
    var e = document.getElementById(id);
    e.style.display = 'none';
}

function customEventCallout(n, m, tu, w, th, f, st, et) {
    var xhr = new XMLHttpRequest();
    var url = myApp.urls.customEvent;
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    var send = {
        "name": n,
        "monday": m,
        "tuesday": tu,
        "wednesday": w,
        "thursday": th,
        "friday": f,
        "startTime": st,
        "endTime": et
    };
    console.log("Payload: " + JSON.stringify(send));
    xhr.send(JSON.stringify(send));

    // When the text is edited, it clears the search and populates it
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            //var text = JSON.parse(xhr.responseText);
            //clearSearch();

            //for (i = 0; i < text.length; i++) {
                //populateSearch(text[i]);
                //console.log(text[i]);
            //}
        }
    }
}

function saveCustomEvent() {
    var name = document.getElementById('custom-event-name').value;
    var monday = document.getElementById('custom-event-monday').checked;
    var tuesday = document.getElementById('custom-event-tuesday').checked;
    var wednesday = document.getElementById('custom-event-wednesday').checked;
    var thursday = document.getElementById('custom-event-thursday').checked;
    var friday = document.getElementById('custom-event-friday').checked;
    var startTime = document.getElementById('custom-event-starttime').value;
    var endTime = document.getElementById('custom-event-endtime').value;

    /*callout function*/
    customEventCallout(name, monday, tuesday, wednesday, thursday, friday, startTime, endTime);

    cancelCustomEvent();

    //generate schedule
}

function cancelCustomEvent() {
    visibility_off('friend-form');

    document.getElementById('custom-event-name').value = document.getElementById('custom-event-name').defaultValue;
    document.getElementById('custom-event-monday').checked = false;
    document.getElementById('custom-event-tuesday').checked = false;
    document.getElementById('custom-event-wednesday').checked = false;
    document.getElementById('custom-event-thursday').checked = false;
    document.getElementById('custom-event-friday').checked = false;
    document.getElementById('custom-event-starttime').value = document.getElementById('custom-event-starttime').defaultValue;
    document.getElementById('custom-event-endtime').value = document.getElementById('custom-event-endtime').defaultValue;
}