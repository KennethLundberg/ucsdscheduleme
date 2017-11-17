// Write your JavaScript code.
function updateMetadata(metadataList)
{

}

function insertMetadata(metadata)
{

}

/*
 * Will clear the calendar of events by removing all elements with class 'event'
 */
function clearMeetings()
{
    /* remove elements with class 'event' and all their children */
    $(".event > div").remove();
}

function insertMeeting(meeting)
{
    /* create an event div */
    var event = $(document.createElement('div'));
    $(event).attr("class","event");

    /* create an event header div and add to event div */
    var eventHeader = $(document.createElement('div'));
    $(eventHeader).attr("class","event-header");
    event.append(eventHeader);

    /* create an icon and add to event header div */
    var icon = $(document.createElement('div'));
    $(icon).attr({
        "class": "icon",
        "id": meeting.kind // TODO: get meeting type from meeting object
    });
    eventHeader.append(icon);

    // alternative way to create element using jquery
    // but pure JS's createElement is faster
    /*
    var icon = $( "<div></div>", {
        "class": "icon",
        "id", meeting.kind
    });
    */

    /* create an event info div */
    var $eventInfo = $(document.createElement('div'));
    $($eventInfo).attr("class","event-info");

    /* create spans from meeting object */
    var $classSpan = $(document.createElement('span'));
    $classSpan.text(meeting.courseAbbreviation);

    var $profSpan = $(document.createElement('span'));
    $profSpan.text( meeting.professor);

    var $timeSpan = $(document.createElement('span'));
    var time = meeting.startTime + " - " + meeting.endTime;
    $timeSpan.text(time);

    var $sectSpan = $(document.createElement('span'));
    $sectSpan.text(meeting.sectionCode);

    /* add spans to event info div */
    $eventInfo.append($classSpan);
    $eventInfo.append($profSpan);
    $eventInfo.append($timeSpan);
    $eventInfo.append($sectSpan);
    eventHeader.append(meeting);

}
 
function updateMeetings(meetings)
{
    for(var i = 0; i < meetings.length; i++) {
        insertMeeting(meetings[i]);
    }
}