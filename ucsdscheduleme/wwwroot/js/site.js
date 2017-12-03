function setup() {
    updateSchedule(myApp.courses);
    console.log("setup()");
}

/* called when DOM is ready */
document.addEventListener('DOMContentLoaded', function () {
    setup();
});


/**
 * @description Gets autocomplete results to populate the search dropdown
 * @param {String} input String typed by the user
 */
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

/**
 * @description Clears the drop down and populates the drop down with the auto-complete results.
 * @param {String} e The string to search for auto-complete results with.
 */
function typeAhead(e) {

    // Sends the input to the server to get the courses
    var input = e.target.value;
    typeAheadCallout(input);
}

/**
 * @description Clears the courses from the drop down.
 */
function clearSearch() {
    var courses = document.getElementsByClassName("courseItem");
    while (courses[0]) {
        courses[0].remove();
    }
}

/**
 * @description Populates the search drop down with the auto-complete results.
 * @param {Number} data The data to populate the drop down with.
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
 * @description Adds event to the course itinerary
 * @param {course/event} event
 * @param {boolean} isCustom Whether the event is a custom event
 */
function addToScheduleList(event, isCustom = false) {
    // Create the element the add to the course list
    var list = document.getElementById("class-list");

    var course = document.createElement('div');
    course.className = (isCustom) ? "custom class" : "class";
    course.id = event.id;

    var span = document.createElement('span');
    span.innerText = event.courseAbbreviation;

    // Create remove button
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

    // Add course to current schedule
    console.log("data.id: " + event.id);
    myApp.coursesToSchedule.push(event.id);
}

/**
 * @description Adds the course selected from the search dropdown to the class itinerary.
 * @param {HTMLElement} data class div selected to add
 */
function addCourse(data) {
    console.log("Data: " + JSON.stringify(data));

    // Hide dropdown menu
    var dropdown = document.getElementById("courseItems");
    dropdown.style.display = "none";

    // Create the element the add to the course list
    var course = { "id": data.id, "courseAbbreviation": data.innerText };
    addToScheduleList(course);

    // Now clear the dropdown and repopulate it
    typeAheadCallout(document.getElementById("search").value);
}

function removeCustomEventCallout(courseId) {
    var xhr = new XMLHttpRequest();
    var url = myApp.urls.removeCustomEvent + "?courseId=" + courseId;
    xhr.open("DELETE", url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    console.log("Payload: " + JSON.stringify(courseId));
    xhr.send();
}

/**
 * @description Removes a course from the schedule of classes
 * @param {HTMLElement} e
 */
function removeCourse(e) {
    var course = e.target.parentNode.parentNode;
    var id = course.id;
    var index = myApp.coursesToSchedule.indexOf(id);
    myApp.coursesToSchedule.splice(index, 1);
    console.log("Courses: " + myApp.coursesToSchedule);

    if (course.classList.contains("custom")) {
        removeCustomEventCallout(id);
        delete myApp.courses[id];
        updateSchedule(myApp.courses);
    }

    course.remove();
    typeAheadCallout(document.getElementById("search").value);
}

/**
 * @description Clears the calendar of events by removing all elements with class 'event'
 */
function clearAllMeetings() {
    /* retrieve elements with class 'event' */
    var elements = document.getElementsByClassName('event');

    /* remove first element in resulting list until all children are deleted*/
    while (elements[0]) {
        elements[0].parentNode.removeChild(elements[0]);
    }
}

/**
 * @description Clears the meetings of one class
 * @param {String} className
 */
function clearMeetings(className) {
    /* retrieve elements with class 'event' */
    var elements = document.getElementsByClassName(className);

    /* remove first element in resulting list until all children are deleted*/
    while (elements[0]) {
        elements[0].parentNode.removeChild(elements[0]);
    }
}

/**
 * @description Based on the time and duration of the event, calculate the top and height of the event element.
 *      This can be done because the height of any 30 minute increment is fixed, so a calculation of the top is just
 *      (# half hour increments after 7:30 am) * (height of individual 30 min increment) in px.
 *      Then height is just (duration of event in minutes) * (height of 30 min section) / 30.
 * @param {Meeting} meeting The meeting to insert
 * @returns The top and height values
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
 * @description Inserts a single meeting into the calendar.
 *  A meeting has the following structure
 *  <div class="event">
 *      <div class="edit-button"><span>Change</span><i class="fa fa-cog" aria-hidden="true"></i></div>
 *      <div class="unlock-button"><i class="fa fa-unlock" aria-hidden="true"></i></div>
 *      <div class="event-header">
 *          <div class="icon" id="lecture">LE</div>
 *          <div class="event-info">
 *              <span>CSE 110</span>
 *              <span>Gary Gillespie</span>
 *              <span>8:00am - 9:20am</span>
 *              <span>A00</span>
 *          </div>
 *      </div>
 *  </div>
 * Creates each div and create the div hiearchy as shown above. Create and add the spans to the "event-info"
 * div. Populate divs and spans with data from the parameter. Use calculateMeetingPosition function to size
 * and place the event div.
 * Each div is assigned the appropriate class and id.
 * @param {Meeting} meeting The meeting to insert
 */
function insertMeeting(meeting, courseId, baseId, sectionId) {
    var isCustomEvent = meeting.type == "CustomEvent";

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

    var editSpan = document.createElement('span');
    editSpan.innerHTML = "Change";
    editSpan.className = "edit-span";
    editButton.append(editSpan);

    var editIcon = document.createElement('i');
    editIcon.className = "fa fa-cog";
    editIcon.setAttribute("aria-hidden", true);
    editButton.append(editIcon);

    if (!isCustomEvent) {
        event.append(editButton);
    }

    /* create an icon label and add to icon div */
    var iconLabel = document.createElement('div');
    iconLabel.className = "class-icon-label";
    //iconLabel.innerText = "LE";
    icon.append(iconLabel);

    // Use first two letters as text
    iconLabel.innerText = meeting.type.toUpperCase().substr(0, 2);

    /* create an event info div */
    var eventInfo = document.createElement('div');
    eventInfo.className = "event-info";

    /** create spans from meeting object **/
    /* courseAbbreviation */
    var classSpan = document.createElement('span');
    classSpan.innerHTML = meeting.courseAbbreviation;

    /* professor */
    var profSpan = document.createElement('span');
    if (!isCustomEvent) {
        profSpan.innerHTML = meeting.professorName;
    }

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

    courseId = ' _c' + courseId;
    baseId = ' _b' + baseId;
    sectionId = ' _s' + sectionId;
    event.className += courseId;
    event.className += baseId;
    event.className += sectionId;

    /* add event to day of meeting */
    var dayElem = document.getElementById(meeting.day.toLowerCase());
    dayElem.append(event);

    return event;
}

/**
 * @description From the list of all bases and sections, get only the selected ones.
 * Then add each event to the calendar by calling insertMeeting on each meeting
 * @param {Meeting} meetings - the JSON object with a list of selected bases, selections
 * See global variable TODO for the structure
 */
function updateMeetings(courses) {
    /* iterate through all the meetings in the JSON */
    for (courseId in courses) {
        var course = courses[courseId];

        /* extract selected base and section - the events to display on calendar */
        var selectedBase = course.selectedBase;
        var selectedSection = course.selectedSection;

        var base = course.bases[selectedBase];

        // Get list of selected bases (i.e. lectures) and section elements (i.e. discussions) */
        var baseEvents = base.baseEvents;

        // If there is no base events for this course, move on.
        if (baseEvents) {
            // Insert all base elements.
            for (var i = 0; i < baseEvents.length; i++) {
                insertMeeting(baseEvents[i], courseId, selectedBase);
            }
        }

        // Check if our course has any section specific events.
        if (base.sectionEvents) {
            var sectionEvents = base.sectionEvents[selectedSection];
            // Check if there are any sections.
            if (sectionEvents) {
                // Insert all section elements.
                for (var i = 0; i < sectionEvents.length; i++) {
                    insertMeeting(sectionEvents[i], courseId, selectedBase, selectedSection);
                }
            }
        }
    }
}

function showAllBasesAndAllSections(ids) {

    var baseEventClass = myApp.constants.baseEventClass;

    var course = myApp.courses[ids.courseId];
    var bases = course.bases;

    var baseKeys = Object.keys(bases);

    clearMeetings(ids.courseId);

    for (var i = 0; i < baseKeys.length; i++) {
        // insert all bases aka lectures
        for (var j = 0; j < bases[baseKeys[i]].baseEvents.length; j++) {
            var meeting = bases[baseKeys[i]].baseEvents[j];

            // insert base and set activated class
            var event = insertMeeting(meeting, ids.courseId, baseKeys[i], baseEventClass);
            event.className += " event-activated";
        }

        // insert each section
        var sectionEvents = bases[baseKeys[i]].sectionEvents;
        var sectionsKeys = Object.keys(sectionEvents);
        sectionsKeys.forEach(function (key) {
            var section = sectionEvents[key];

            // insert section and set activated class
            for (var k = 0; k < section.length; k++) {
                var event = insertMeeting(section[k], ids.courseId, baseKeys[i], key);
                event.className += " event-activated";
            }
        });
    }
}

function showBaseAndAllSections(ids) {
    var baseEventClass = myApp.constants.baseEventClass;
    var course = myApp.courses[ids.courseId];
    var baseEvents = course.bases[ids.baseId].baseEvents;
    var sectionEvents = course.bases[ids.baseId].sectionEvents;

    clearMeetings(ids.courseId);

    for (var i = 0; i < baseEvents.length; i++) {
        var event = insertMeeting(baseEvents[i], ids.courseId, ids.baseId, baseEventClass);
        event.className += " event-activated";
    }

    var sectionsKeys = Object.keys(sectionEvents);
    sectionsKeys.forEach(function (key) {
        var section = sectionEvents[key];
        var event = insertMeeting(section[0], ids.courseId, ids.baseId, key);
        event.className += " event-activated";
    });

    hideEditButtons();
}

function changeSchedule(event) {
    var info = extractEventInfo(event);

    // base selected
    if (info.isBaseEvent) {
        showAllBasesAndAllSections(info);
    }
    // section selected
    else {
        showBaseAndAllSections(info);
    }
}

function changeScheduleSectionCallout(oldSectionId, newSectionId) {
    console.log("changeScheduleSectionCallout");
    var xhr = new XMLHttpRequest();
    var url = myApp.urls.changeScheduleSection;
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    var request = { "sectionIdToRemove": oldSectionId, "sectionIdToAdd": newSectionId };
    xhr.send(JSON.stringify(request));
}

/**
 * updateSelectedSection
 * @param: sectionId: string
 */
function updateSelectedSection(event) {
    var ids = extractEventInfo(event);

    var course = myApp.courses[ids.courseId];
    var currentBase = course.selectedBase;
    var currentSectionId = course.selectedSection;

    // update the base and section IDs in the global object
    course.selectedSection = ids.sectionId;
    course.selectedBase = ids.baseId;

    clearAllMeetings();
    updateMeetings(myApp.courses);

    isEditing = false;
    console.log("before changeScheduleSectionCallout");
    changeScheduleSectionCallout(currentSectionId, ids.sectionId);
    showEditButtons();
}

/**
 * @param: baseId: string, sectionId: string
 */
function updateSelectedBase(event) {
    var ids = extractEventInfo(event);

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

// clicked on section or base, as defined by extractEventInfo
function updateEvent(event) {
    var info = extractEventInfo(event);
    if (!info.isBaseEvent) {
        // selected a section
        updateSelectedSection(event);
    } else {
        // selected a base
        updateSelectedBase(event)
    }
}

function activateSelectedBasesAndSections(event) {
    var infoForSelected = extractEventInfo(event, false);

    var allActivatedEvents = document.getElementsByClassName('event-activated');

    if (!infoForSelected.isBaseEvent) {
        var toDeactivate = [];
        for (var j = 0; j < allActivatedEvents.length; j++) {
            var classList = allActivatedEvents[j].classList;
            var infoForCurrent = extractEventInfo(allActivatedEvents[j], false);

            if (classList.contains(infoForSelected.courseId) && classList.contains(infoForSelected.baseId)) {
                if (classList.contains(infoForSelected.sectionId) || infoForCurrent.isBaseEvent) {
                } else {
                    toDeactivate.push(allActivatedEvents[j]);
                }
            } else {
                toDeactivate.push(allActivatedEvents[j]);
            }
        }

        for (var k = 0; k < toDeactivate.length; k++) {
            toDeactivate[k].classList.add('event-deactivated');
            toDeactivate[k].classList.remove('event-activated');
        }

    } else {
        var toDeactivate = [];

        for (var j = 0; j < allActivatedEvents.length; j++) {
            var classList = allActivatedEvents[j].classList;

            if (!classList.contains(infoForSelected.courseId) || !classList.contains(infoForSelected.baseId)) {
                toDeactivate.push(allActivatedEvents[j]);
            }
        }

        for (var k = 0; k < toDeactivate.length; k++) {
            toDeactivate[k].classList.add('event-deactivated');
            toDeactivate[k].classList.remove('event-activated');
        }
    }
}

function reactivateAllBasesAndAllSections(event) {
    var ids = extractEventInfo(event, false);
    var allActivatedEvents = document.getElementsByClassName('event-deactivated');
    while (allActivatedEvents.length > 0) {
        allActivatedEvents[0].className += ' event-activated';
        allActivatedEvents[0].classList.remove('event-deactivated');
    }
}

// TODO: Replace comments
// default: returns no underscores
function extractEventInfo(event, noUnderscore = true) {

    if (!event || !event.classList || event.classList.length <= 0) {
        return null;
    }
    var classListLength = event.classList.length;

    var prefixLength = 2;

    var constants = myApp.constants;

    var courseId = findClassWithPrefix(event, constants.coursePrefix, prefixLength);
    var baseId = findClassWithPrefix(event, constants.basePrefix, prefixLength);
    var sectionId = findClassWithPrefix(event, constants.sectionPrefix, prefixLength);

    var isBaseEvent = sectionId === constants.sectionPrefix + constants.baseEventClass;

    if (noUnderscore) {
        courseId = courseId.substr(prefixLength);
        baseId = baseId.substr(prefixLength);
        sectionId = sectionId.substr(prefixLength);
    }

    return {
        courseId: courseId,
        baseId: baseId,
        sectionId: sectionId,
        isBaseEvent: isBaseEvent
    };
}

function findClassWithPrefix(element, prefix, prefixLength) {
    var classes = element.classList;
    var classWithPrefix = Array.from(classes).find(cl => cl.substr(0, prefixLength) == prefix);
    return classWithPrefix;
}

/**
 * @param: element: DOM element of which you want to find the event div for 
 */
function findOuterDiv(element, className) {

    if (typeof element == undefined) {
        return null;
    }

    while (element && element.classList && !element.classList.contains(className)) {
        element = element.parentNode;
    }

    if (element && element.classList && element.classList.contains(className)) {
        return element;
    }

    return null;
}

function hideEditButtons() {
    var buttons = document.getElementsByClassName('edit-button');
    for (var i = 0; i < buttons.length; i++) {
        buttons[i].style.visibility = 'hidden';
    }
}

function showEditButtons() {
    var buttons = document.getElementsByClassName('edit-button');
    for (var i = 0; i < buttons.length; i++) {
        buttons[i].style.visibility = 'visible';
    }
}

/**
 * @description Clear method that removes all divs for individual classes and its children.Also clearing the table of overall metadata.
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
 * @description Clears the overall metadata table seting the values to 0.
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
 * @description A function that takes in a metadata object for an individual course and adds it to the view.
 * @param {Metadata} metadata Metadata object to insert
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
    rateMyProfessor.className = "professor-name";
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

    // If there is no data, do not append
    if (metadata.quality != 0 && metadata .difficulty != 0 
        && metadata.averageWorkload != 0 && metadata.averageGpaExpected != 0
        && metadata.averageGpaReceived != 0) {
        courseMetadata.append(rateMyProfessor);
        courseMetadata.append(avgWorkload);
        courseMetadata.append(avgExpected);
        courseMetadata.append(avgRecieved);
    }

    //retrieving course-stat-container
    var courseGoesHere = document.getElementById("course-stat-container");

    //insert course container to right side bar
    courseGoesHere.append(courseMetadata);
}

/**
 * @description A function that updates the metadata by calling the InsertMetadata function for each course metadata
 * @param {ScheduleViewModel} courses Courses to display metadata
 */
function updateMetadata(courses) {
    /* iterate through all the meetings in the JSON */
    for (courseId in courses) {
        var course = courses[courseId];
        /* extract selected base - the events to display on calendar */
        var selectedBase = course.selectedBase;

        /* get list of one time events (i.e. finals) */
        var metadata = course.bases[selectedBase].metadata;

        // If there is no metadata for this class, skip over it.
        if (!metadata) {
            continue;
        }

        /* insert metadata */
        insertMetadata(metadata);
    }
}

/**
 * @description A function that updates the overall metadata table in the view by iterating through the list of metadata and calculating the new overall data
 * @param {ScheduleViewModel} courses Courses to display metadata
 */
function updateOverallMetadata(courses) {
    var numCourses = 0;
    var overallWorkload = 0;
    var overallExpectedGpa = 0;
    var overallReceivedGpa = 0;

    //iterate through all the metadata in the JSON
    for (courseId in courses) {
        var course = courses[courseId];
        /* extract selected base - the events to display on calendar */
        var selectedBase = course.selectedBase;

        /* get list of one time events (i.e. finals) */
        var metadata = course.bases[selectedBase].metadata;

        // If there is no metadata for this course, skip it.
        if (!metadata || metadata.averageWorkload === 0 
            || metadata.averageGpaExpected === 0 || metadata.averageGpaReceived === 0) {
            continue;
        }

        overallWorkload += metadata.averageWorkload;
        overallExpectedGpa += metadata.averageGpaExpected;
        overallReceivedGpa += metadata.averageGpaReceived;

        numCourses++;
    }

    //calculating averages
    overallExpectedGpa /= numCourses;
    overallReceivedGpa /= numCourses;

    document.getElementById("overall-workload").innerHTML = overallWorkload.toFixed(2) + " Hr/wk";
    document.getElementById("gpa-expected").innerHTML = convertGPAToStringFormat(overallExpectedGpa.toFixed(2));
    document.getElementById("gpa-received").innerHTML = convertGPAToStringFormat(overallReceivedGpa.toFixed(2));
}

/**
 * @description Helper function that takes GPA decimal and returns equivalent letter grade in proper format.
 * @param {Number} grade Gpa grade
 */
function convertGPAToStringFormat(grade) {
    var prefix = "";

    if (grade >= 4.0) {
        prefix = "A";
    }
    else if (grade >= 3.7) {
        prefix = "A-";
    }
    else if (grade >= 3.3) {
        prefix = "B+";
    }
    else if (grade >= 3.0) {
        prefix = "B";
    }
    else if (grade >= 2.7) {
        prefix = "B-";
    }
    else if (grade >= 2.3) {
        prefix = "C+";
    }
    else if (grade >= 2.0) {
        prefix = "C";
    }
    else if (grade >= 1.7) {
        prefix = "C-";
    }
    else if (grade >= 1.0) {
        prefix = "D";
    }
    else {
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
        var course = courses[courseId];

        /* extract selected base - the events to display on calendar */
        var selectedBase = course.selectedBase;

        /* get list of one time events (i.e. finals) */
        var oneTimeEvents = course.bases[selectedBase].oneTimeEvents;

        // If there are no one time events for this course, go to the next one.
        if (!oneTimeEvents) {
            continue;
        }

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
    clearAllMeetings();
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

function customEventCallout(name, days, startTime, endTime) {
    var xhr = new XMLHttpRequest();
    var url = myApp.urls.customEvent;
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    var send = {
        "name": name,
        "days": days,
        "startTime": startTime,
        "endTime": endTime
    };

    //TODO check valid input

    console.log("Payload: " + JSON.stringify(send));
    xhr.send(JSON.stringify(send));

    // Generate schedule with new event
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            console.log("Custom Event: " + JSON.stringify(xhr.responseText));
            var text = JSON.parse(xhr.responseText);

            //get course id to add to scheduleing
            var course = { "id": text.courseId, "courseAbbreviation": text.courseAbbreviation };
            addToScheduleList(course, true);

            var scheduleCourse = {
                "selectedBase": "A",
                "selectedSection": text.sectionId,
                "bases": {
                    "A": {
                        "sectionEvents": {

                        }
                    }
                }
            };

            scheduleCourse.bases["A"].sectionEvents[text.sectionId] = text.calendarEvents;

            myApp.courses[text.courseId] = scheduleCourse;

            updateSchedule(myApp.courses);
        }
    }
}

function saveCustomEvent() {
    var name = document.getElementById('custom-event-name').value;
    var monday = document.getElementById('custom-event-monday').checked << 1;
    var tuesday = document.getElementById('custom-event-tuesday').checked << 2;
    var wednesday = document.getElementById('custom-event-wednesday').checked << 3;
    var thursday = document.getElementById('custom-event-thursday').checked << 4;
    var friday = document.getElementById('custom-event-friday').checked << 5;
    var startTime = document.getElementById('custom-event-starttime').value;
    var endTime = document.getElementById('custom-event-endtime').value;

    var days = monday | tuesday | wednesday | thursday | friday;

    /*callout function*/
    customEventCallout(name, days, startTime, endTime);

    closeCustomEvent();
}

function closeCustomEvent() {
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