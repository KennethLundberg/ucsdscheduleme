// Write your JavaScript code.
function updateMetadata(metadataList)
{

}

function insertMetadata(metadata)
{

}


document.addEventListener('DOMContentLoaded', function() {
    clearMeetings();
    setup();
});

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
 * Creates each div and create the div hiearchy as shown above. Create and add the spans to the "event-info"
 * div. Populate divs and spans with data from the parameter.
 * Based on the time and duration of the event, calculate the top and height of the event element.
 *      This can be done because the height of any 30 minute increment is fixed, so a calculation of the top is just
 *      (# half hour increments after 7:30 am) * (height of individual 30 min increment) in px.
 *      Then height is just (duration of event in minutes) * (height of 30 min section) / 30.
 *  Each div is assigned the appropriate class and id.
 */
function insertMeeting(meeting)
{
    /* calculate top and height based on number of half hour increments after 7:30am and duration */
    var numHalfHourInc = (meeting.StartTimeInMinutesAfterFirstHour) / 30;
    var height30MinInc = document.querySelector(".time").childNodes[1].offsetHeight;
    var timeOffSet = 50;

    var top = (numHalfHourInc * height30MinInc + timeOffSet) + "px";
    var height = ((meeting.DurationInMinutes) * (height30MinInc) / 30) + "px";

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
    icon.className = "icon";
    icon.id = meeting.type;
    eventHeader.append(icon);

    /* create an event info div */
    var eventInfo = document.createElement('div');
    eventInfo.className = "event-info";

    /* create spans from meeting object */

    /* courseAbbreviation */
    var classSpan = document.createElement('span');
    classSpan.innerHTML = meeting.courseAbbreviation;

    /* professor */
    var profSpan = document.createElement('span');
    profSpan.innerHTML = meeting.professor;

    /* time range */
    var timeSpan = document.createElement('span');
    timeSpan.innerHTML = meeting.Timespan;

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

/**
 * test adding calendar events, will delete later
 **/
function setup() {
    console.log("setup()");
    var meetings2 = new Array();

    var cse20lectureMonday = {
        type: "lecture",
        courseAbbreviation: "CSE 20",
        professor: "Miles, Jones",
        code: "A02",
        StartTimeInMinutesAfterFirstHour: 30,
        DurationInMinutes: 80,
        Timespan: "8:00am - 9:20am",
        Day: "monday"
    };
    var cse20lectureWed = {
        type: "lecture",
        courseAbbreviation: "CSE 20",
        professor: "Miles, Jones",
        code: "A02",
        StartTimeInMinutesAfterFirstHour: 30,
        DurationInMinutes: 80,
        Timespan: "8:00am - 9:20am",
        Day: "wednesday"
    };
    var cse20disTu = {
        type: "discussion",
        courseAbbreviation: "CSE 20",
        professor: "Miles, Jones",
        code: "A02",
        StartTimeInMinutesAfterFirstHour: 60,
        DurationInMinutes: 60,
        Timespan: "8:30am - 9:30pm",
        Day: "tuesday"
    };

    var cse100lectureWed = {
        type: "lecture",
        courseAbbreviation: "CSE 100",
        professor: "Alvarado, Christine",
        code: "A12",
        StartTimeInMinutesAfterFirstHour: 120,
        DurationInMinutes: 90,
        Timespan: "9:30am - 11:00am",
        Day: "wednesday"
    };

    var cse100lectureFri = {
        type: "lecture",
        courseAbbreviation: "CSE 100",
        professor: "Alvarado, Christine",
        code: "A12",
        StartTimeInMinutesAfterFirstHour: 0,
        DurationInMinutes: 60,
        Timespan: "7:30am - 8:30am",
        Day: "friday"
    };

    var cse110lectureThur = {
        type: "lecture",
        courseAbbreviation: "CSE 110",
        professor: "Gary Gilesspie",
        code: "C02",
        StartTimeInMinutesAfterFirstHour: 60,
        DurationInMinutes: 60,
        Timespan: "8:30am - 9:30am",
        Day: "thursday"
    };
    var cse110labThur = {
        type: "lab",
        courseAbbreviation: "CSE 110",
        professor: "Gary Gilesspie",
        code: "C02",
        StartTimeInMinutesAfterFirstHour: 210,
        DurationInMinutes: 180,
        Timespan: "11:00am - 2:00pm",
        Day: "tuesday"
    };

    var cse112labThur = {
        type: "lab",
        courseAbbreviation:  "CSE 112",
        professor: "Mystery",
        code: "A23",
        StartTimeInMinutesAfterFirstHour: 390,
        DurationInMinutes: 120,
        Timespan: "2:00pm - 4:00pm",
        Day: "thursday"
    };

    meetings2[0] = cse20lectureMonday;
    meetings2[1] = cse20lectureWed;
    meetings2[2] = cse20disTu;
    meetings2[3] = cse100lectureWed;
    meetings2[4] = cse100lectureFri;
    meetings2[5] = cse110lectureThur;
    meetings2[6] = cse110labThur;
    meetings2[7] = cse112labThur;

    updateMeetings(meetings2);
}