// Write your JavaScript code.
function updateMetadata(metadataList)
{

}

function insertMetadata(metadata)
{

}


/*
 * Function: clearMeetings()
 * Param: none
 * Description: Will clear the calendar of events by removing all elements with class 'event'
 */
function clearMeetings()
{
    /* remove elements with class 'event' and all their children */
    $(".event > div").remove();
}

/*
 * Function: insertMeeting(meeting)
 * Param: meeting - the meeting to insert
 * Description: Inserts a single meeting into the calendar.
 *  A meeting has the following structure
    <div class="event">
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
 *  Based on the time and duration of the event, calculate the top and height of the event element.
 *      This can be done because the height of any 30 minute increment is fixed, so a calculation of the top is just
 *      (# half hour increments after 7:30 am) * (height of individual 30 min increment) in px.
 *      Then height is just (duration of event in minutes) * (height of 30 min section) / 30.
 *  Each div is assigned the appropriate class and id.
 */
function insertMeeting(meeting)
{
    // TODO based on the time and duration of the event, calculate the top and height of the event element
    var numHalfHourInc = (meeting.DurationInMinutes) / 30;
    var height30MinInc = 30;
    var top = numHalfHourInc * height30MinInc;
    var height = (meeting.DurationInMinutes) * (height30MinInc) / 30;

    
    /* create an event div */
    var event = document.createElement('div');
    event.style.top = 100;
    event.className = "event";

    /* create an event header div and add to event div */
    var eventHeader = document.createElement('div');
    eventHeader.className = "event-header";
    event.append(eventHeader);

    /* create an icon and add to event header div */
    var icon = document.createElement('div');
    icon.innerHTML = meeting.type;
    eventHeader.append(icon);

    /* create an event info div */
    var eventInfo = document.createElement('div');
    eventInfo.className = "event-info";

    /* create spans from meeting object */
    var classSpan = document.createElement('span');
    classSpan.innerHTML = meeting.courseAbbreviation;

    var profSpan = document.createElement('span');
    profSpan.innerHTML = meeting.professor;

    var timeSpan = document.createElement('span');
    timeSpan.innerHTML = meeting.Timespan;

    var sectSpan = document.createElement('span');
    sectSpan.innerHTML = meeting.code;

    /* add spans to event info div */
    eventInfo.append(classSpan);
    eventInfo.append(profSpan);
    eventInfo.append(timeSpan);
    eventInfo.append(sectSpan);
    eventHeader.append(eventInfo);

    /* add event to day of meeting */
    var dayElem = document.getElementById(meeting.Day);
    dayElem.append(event);
}

/*
 * Function: updateMeetings(meetings)
 * Param: meetings - an array of meetings to insert
 * Description: Adds each meeting to the calendar by calling insertMeeting on each element
 */
function updateMeetings(meetings)
{
    for(var i = 0; i < meetings.length; i++) {
        insertMeeting(meetings[i]);
    }
}