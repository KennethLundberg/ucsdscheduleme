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

var courses1 = {
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

var courses = {
    "cse101": {
        "selectedBase": "B",
        "selectedSection": "10201",
        "bases": {
            "A": {
                //Array of base object calenar events here (like lectures)
                "baseElements": [
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A01",
                        startTimeInMinutesAfterFirstHour: 30,
                        durationInMinutes: 50,
                        timespan: "8:00am - 8:50am",
                        day: "monday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A01",
                        startTimeInMinutesAfterFirstHour: 30,
                        durationInMinutes: 50,
                        timespan: "8:00am - 8:50am",
                        day: "wednesday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A01",
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
                            code: "A01-1",
                            startTimeInMinutesAfterFirstHour: 30,
                            durationInMinutes: 50,
                            timespan: "8:00am - 8:50am",
                            day: "tuesday"
                        }
                    ],
                    "91023": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 101",
                            professor: "Miles, Jones",
                            code: "A01-2",
                            startTimeInMinutesAfterFirstHour: 90,
                            durationInMinutes: 50,
                            timespan: "9:00am - 9:50am",
                            day: "thursday"
                        }
                    ],
                    "91047": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 101",
                            professor: "Miles, Jones",
                            code: "A01-3",
                            startTimeInMinutesAfterFirstHour: 90,
                            durationInMinutes: 50,
                            timespan: "9:00am - 9:50am",
                            day: "wednesday"
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
                        code: "A02",
                        startTimeInMinutesAfterFirstHour: 150,
                        durationInMinutes: 50,
                        timespan: "10:00am - 10:50am",
                        day: "tuesday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "A02",
                        startTimeInMinutesAfterFirstHour: 150,
                        durationInMinutes: 50,
                        timespan: "10:00am - 10:50am",
                        day: "thursday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "10201": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 101",
                            professor: "Miles, Jones",
                            code: "A02-1",
                            startTimeInMinutesAfterFirstHour: (1*60+30),
                            durationInMinutes: 50,
                            timespan: "9:00am - 9:50am",
                            day: "friday"
                        }
                    ],
                    "11023": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 101",
                            professor: "Miles, Jones",
                            code: "A02-2",
                            startTimeInMinutesAfterFirstHour: (1*60+30),
                            durationInMinutes: 50,
                            timespan: "9:00am - 9:50am",
                            day: "monday"
                        }
                    ]
                },
                "metadata" : {
                    //the actual object here
                }
            }
        }
    }
}

/**
 * test adding calendar events, will delete later
 **/
function setup() {
    clearMeetings();
    //console.log("setup()");
    updateMeetings(courses);
}

/* called when DOM is ready */
document.addEventListener('DOMContentLoaded', function() {
    setup();
});

/** END OF DELETE **/


/*
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

/*
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

/*
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
    icon.id = meeting.type;
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

    courseId = ' _' + courseId;
    baseId = ' _' + baseId;
    sectionId = ' _' + sectionId;
    event.className += courseId;
    event.className += baseId;
    event.className += sectionId;

    /* add event to day of meeting */
    var dayElem = document.getElementById(meeting.day);
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
function updateMeetings(meetings)
{
    /* iterate through all the meetings in the JSON */
    for(meeting in meetings) {
        /* extract selected base and section - the events to display on calendar */
        var selectedBase = courses[meeting].selectedBase;
        var selectedSection = courses[meeting].selectedSection;

        /* get list of selected bases (i.e. lectures) and section elements (i.e. discussions) */
        var baseElements = courses[meeting].bases[selectedBase].baseElements;
        var sectionElements = courses[meeting].bases[selectedBase].sectionElements[selectedSection];

        /* insert all base elements */
        for(var i = 0; i < baseElements.length; i++) {
            insertMeeting(baseElements[i], meeting, selectedBase);
        }

        /* check if there are any sections */
        if(sectionElements != null) {

            /* insert all section elements */
            for(var i = 0; i < sectionElements.length; i++) {
                insertMeeting(sectionElements[i], meeting, selectedBase, selectedSection);
            }
        }
    }
}

function showAllBasesAndAllSections(ids) {
    var course = courses[ids.courseId];
    var bases = course.bases;

    // baseKeys is 'A', 'B' // TODO: Replace with proper comments
    var baseKeys = Object.keys(bases);

    for(var i = 0; i < baseKeys.length; i++) {
        // insert all bases aka lectures
        for(var j = 0; j < bases[baseKeys[i]].baseElements.length; j++) {
            var meeting = bases[baseKeys[i]].baseElements[j];

            // insert base and set activated class
            var event = insertMeeting(meeting, ids.courseId, baseKeys[i], "undefined");
            event.className += " event-activated";
        }

        // insert each section
        var sectionElements = bases[baseKeys[i]].sectionElements;
        var sectionsKeys = Object.keys(sectionElements);
        sectionsKeys.forEach(function(key) {
            var section = sectionElements[key];

            // insert section and set activated class
            for(var k = 0; k < section.length; k++) {
                var event = insertMeeting(section[k], ids.courseId, baseKeys[i], key);
                event.className += " event-activated";
            }
        });
    }
}

function showBaseAndAllSections(ids) {
    var course = courses[ids.courseId];
    var baseElements = course.bases[ids.baseId].baseElements;
    var sectionElements = course.bases[ids.baseId].sectionElements;

    for(var i = 0; i < baseElements.length; i++) {
        var event = insertMeeting(baseElements[i], ids.courseId, ids.baseId, "undefined");
        event.className += " event-activated";
    }

    var sectionsKeys = Object.keys(sectionElements);
    sectionsKeys.forEach(function(key) {
        var section = sectionElements[key];
        var event = insertMeeting(section[0], ids.courseId, ids.baseId, key);
        event.className += " event-activated";
    });
}

function changeSchedule(event) {
    console.log("changeSchedule ")
    // console.log(event);

    var ids = extractIds(event);
    console.log(ids);

    // base selected
    if(ids.sectionId === "undefined") {
        console.log("base")
        showAllBasesAndAllSections(ids);
    } 
    // section selected
    else {
        console.log("section")
        showBaseAndAllSections(ids);
    }
}
/**
 * updateSelectedSection
 * @param: sectionId: string
 */
function updateSelectedSection(event) {
    

    var ids = extractIds(event);
    courses[ids.courseId].selectedSection = ids.sectionId;
    courses[ids.courseId].selectedBase = ids.baseId;

    // console.log("updateSelectedSection " + ids.sectionId);
    // console.log("base id " + ids.baseId);
    clearMeetings();
    updateMeetings(courses);
    isEditing = false;
}

/**
 * @param: baseId: string, sectionId: string
 */
function updateSelectedBase(event) {
    var ids = extractIds(event);
    // console.log(ids)
    courses[ids.courseId].selectedBase = ids.baseId;

    // console.log("updateSelectedBase " + ids.baseId)
    // console.log(courses[ids.courseId].selectedBase)
    clearMeetings();
    updateMeetings(courses)
    showBaseAndAllSections(ids);    
}


function updateEvent(event) {
    if(!event.classList.contains("_undefined")) {
        updateSelectedSection(event);
    } else {
        updateSelectedBase(event)
    }
}

function extractIds(event) {

    if(event.classList.length <= 0) {
        return null;
    }

    var courseId = event.classList[1].substr(1);
    var baseId = event.classList[2].substr(1);
    var sectionId = event.classList[3].substr(1);

    return {
        courseId: courseId,
        baseId: baseId,
        sectionId: sectionId
    }
}

/**
 * @param: element: DOM element of which you want to find the event div for 
 */
function findEventDiv(element) {
    // console.log(element.classList.length);

    // if(element.classList.length > 0) {
        while(!element.classList.contains('event')) {
            element = element.parentNode;
        }
    // }

    if(element.classList.contains('event')) { 
        return element;
    }

    return null;
}


