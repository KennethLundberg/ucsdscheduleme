/** START OF DELETE **/

var courses = {
    "cse101": {
        "selectedBase": "B",
        "selectedSection": "10201",
        "bases": {
            "A": {
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
                    "92047": [
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
                        code: "B00",
                        startTimeInMinutesAfterFirstHour: 150,
                        durationInMinutes: 50,
                        timespan: "10:00am - 10:50am",
                        day: "tuesday"
                    },
                    {
                        type: "lecture",
                        courseAbbreviation: "CSE 101",
                        professor: "Miles, Jones",
                        code: "B00",
                        startTimeInMinutesAfterFirstHour: 150,
                        durationInMinutes: 50,
                        timespan: "10:00am - 10:50am",
                        day: "thursday"
                    }
                ],
                "sectionElements": {
                    "10201": [
                        {
                            type: "discussion",
                            courseAbbreviation: "CSE 101",
                            professor: "Miles, Jones",
                            code: "B01",
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
                            code: "B02",
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
    },
    "cse110": {
        "selectedBase": "C",
        "selectedSection": "C0",
        "bases": {
            "C": {
                "baseElements": [

                ],
                "sectionElements": {
                    "C0": [
                        {
                            type: "lecture",
                            courseAbbreviation: "CSE 110",
                            professor: "Gillespie, Gary",
                            code: "C00",
                            startTimeInMinutesAfterFirstHour: 5*60,
                            durationInMinutes: 110,
                            timespan: "12:30pm - 1:50pm",
                            day: "tuesday"
                        }
                    ],
                }
            },
            "D": {
                "baseElements": [
                ],
                "sectionElements": {
                    "D00": [
                        {
                            type: "lecture",
                            courseAbbreviation: "CSE 110",
                            professor: "Gillespie, Gary",
                            code: "D00",
                            startTimeInMinutesAfterFirstHour: 3*60,
                            durationInMinutes: 110,
                            timespan: "10:30am - 11:50am",
                            day: "thursday"
                        }
                    ]
                }
            }
        }
    }
}
/**
 * test adding calendar events, will delete later
 **/
function setup() {
    clearAllMeetings();
    //console.log("setup()");
    updateMeetings(courses);
}
 
/* called when DOM is ready */
document.addEventListener('DOMContentLoaded', function() {
    setup();
});


/** END OF DELETE **/


/*
 * Function: clearAllMeetings()
 * Param: none
 * Description: Clears the calendar of events by removing all elements with class 'event'
 */
function clearAllMeetings()
{
    /* retrieve elements with class 'event' */
    var elements = document.getElementsByClassName('event');

    /* remove first element in resulting list until all children are deleted*/
    while(elements[0]) {
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
        var selectedBase = meetings[meeting].selectedBase;
        var selectedSection = meetings[meeting].selectedSection;

        /* get list of selected bases (i.e. lectures) and section elements (i.e. discussions) */
        var baseElements = meetings[meeting].bases[selectedBase].baseElements;
        var sectionElements = meeting[meeting].bases[selectedBase].sectionElements[selectedSection];

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

    var course = myApp.courses[ids.courseId];
    var bases = course.bases;

    var baseKeys = Object.keys(bases);

    clearMeetings(ids.courseId);

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
    var course = myApp.courses[ids.courseId];
    var baseElements = course.bases[ids.baseId].baseElements;
    var sectionElements = course.bases[ids.baseId].sectionElements;
    
    clearMeetings(ids.courseId);

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
    var baseElements = myApp.courses[ids.courseId].bases[ids.baseId].baseElements;
    var sectionElements = myApp.courses[ids.courseId].bases[ids.baseId].sectionElements;

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

    while(!element.classList.contains(className)) {
        element = element.parentNode;
    }

    if(element.classList.contains(className)) { 
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
