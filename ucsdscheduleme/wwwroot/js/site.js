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
                        Type: "lecture",
                        CourseAbbreviation: "CSE 101",
                        Professor: "Miles, Jones",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: 30,
                        DurationInMinutes: 50,
                        Timespan: "8:00am - 8:50am",
                        Day: "monday"
                    },
                    {
                        Type: "lecture",
                        CourseAbbreviation: "CSE 101",
                        Professor: "Miles, Jones",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: 30,
                        DurationInMinutes: 50,
                        Timespan: "8:00am - 8:50am",
                        Day: "wednesday"
                    },
                    {
                        Type: "lecture",
                        CourseAbbreviation: "CSE 101",
                        Professor: "Miles, Jones",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: 30,
                        DurationInMinutes: 50,
                        Timespan: "8:00am - 8:50am",
                        Day: "friday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "90210": [
                        {
                            Type: "discussion",
                            CourseAbbreviation: "CSE 101",
                            Professor: "Miles, Jones",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (10*60+30),
                            DurationInMinutes: 110,
                            Timespan: "6:00pm - 7:50pm",
                            Day: "tuesday"
                        }
                    ],
                    "91023": [
                        {
                            Type: "discussion",
                            CourseAbbreviation: "CSE 101",
                            Professor: "Miles, Jones",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (11*60+30),
                            DurationInMinutes: 50,
                            Timespan: "7:00pm - 8:50pm",
                            Day: "tuesday"
                        }
                    ]
                },
                "metadata": [
                    {
                        CourseAbbreviation: "CSE 110",
                        Professor: "Gary Gillespie",
                        AvgWorkload: "Avg. Workload: 69 Hrs/Wk ",
                        AvgGradeExpected: "Avg. Grade Expected: F- (0.00)",
                        AvgGradeReceived: "Avg. Grade Received: F- (0.00)",
                    }
                ]
            },
            "B": {
                "baseElements": [
                    {
                        Type: "lecture",
                        CourseAbbreviation: "CSE 101",
                        Professor: "Miles, Jones",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: 150,
                        DurationInMinutes: 50,
                        Timespan: "10:00am - 10:50am",
                        Day: "monday"
                    },
                    {
                        Type: "lecture",
                        CourseAbbreviation: "CSE 101",
                        Professor: "Miles, Jones",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: 150,
                        DurationInMinutes: 50,
                        Timespan: "10:00am - 10:50am",
                        Day: "wednesday"
                    },
                    {
                        Type: "lecture",
                        CourseAbbreviation: "CSE 101",
                        Professor: "Miles, Jones",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: 150,
                        DurationInMinutes: 50,
                        Timespan: "10:00am - 10:50am",
                        Day: "friday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "10201": [
                        {
                            Type: "discussion",
                            CourseAbbreviation: "CSE 101",
                            Professor: "Miles, Jones",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (10*60+30),
                            DurationInMinutes: 110,
                            Timespan: "6:00pm - 7:50pm",
                            Day: "thursday"
                        }
                    ],
                    "11023": [
                        {
                            Type: "discussion",
                            CourseAbbreviation: "CSE 101",
                            Professor: "Miles, Jones",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (11*60+30),
                            DurationInMinutes: 50,
                            Timespan: "7:00pm - 8:50pm",
                            Day: "monday"
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
                        Type: "lecture",
                        CourseAbbreviation: "CSE 100",
                        Professor: "Alvarado, Christine",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: (3*60+1*30),
                        DurationInMinutes: 80,
                        Timespan: "11:0am-12:20pm",
                        Day: "tuesday"
                    },
                    {
                        Type: "lecture",
                        CourseAbbreviation: "CSE 100",
                        Professor: "Alvarado, Christine",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: (3*60+1*30),
                        DurationInMinutes: 80,
                        Timespan: "11:0am-12:20pm",
                        Day: "thursday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "90210": [
                        {
                            Type: "discussion",
                            CourseAbbreviation: "CSE 100",
                            Professor: "Alvarado, Christine",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (10*60+30),
                            DurationInMinutes: 50,
                            Timespan: "6:00pm - 6:50pm",
                            Day: "friday"
                        }
                    ],
                    "91023": [
                        {
                            Type: "discussion",
                            CourseAbbreviation: "CSE 100",
                            Professor: "Alvarado, Christine",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (11*60+30),
                            DurationInMinutes: 50,
                            Timespan: "7:00pm - 8:50pm",
                            Day: "monday"
                        }
                    ]
                },
                "metadata": [
                
                ]
            },
            "B": {
                "baseElements": [
                    {
                        Type: "lecture",
                        CourseAbbreviation: "CSE 100",
                        Professor: "Alvarado, Christine",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: (5*60+1*30),
                        DurationInMinutes: 80,
                        Timespan: "12:00pm-1:20pm",
                        Day: "tuesday"
                    },
                    {
                        Type: "lecture",
                        CourseAbbreviation: "CSE 100",
                        Professor: "Alvarado, Christine",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: (5*60+1*30),
                        DurationInMinutes: 80,
                        Timespan: "12:00pm-1:20pm",
                        Day: "thursday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "10201": [
                        {
                            Type: "discussion",
                            CourseAbbreviation: "CSE 100",
                            Professor: "Alvarado, Christine",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (9*60+30),
                            DurationInMinutes: 50,
                            Timespan: "5:00pm - 5:50pm",
                            Day: "thursday"
                        }
                    ],
                    "11023": [
                        {
                            Type: "discussion",
                            CourseAbbreviation: "CSE 100",
                            Professor: "Alvarado, Christine",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (8*60+30),
                            DurationInMinutes: 50,
                            Timespan: "4:00pm - 4:50pm",
                            Day: "monday"
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
                        Type: "lecture",
                        CourseAbbreviation: "CSE 100",
                        Professor: "Gillespie, Gary",
                        Code: "A00",
                        StartTimeInMinutesAfterFirstHour: (9*60+1*30),
                        DurationInMinutes: 170,
                        Timespan: "5:00pm-7:50pm",
                        Day: "tuesday"
                    }
                ],
                "sectionElements": {
                    //Array of this section specific courses here
                    "90210": [
                        {
                            Type: "lab",
                            CourseAbbreviation: "CSE 100",
                            Professor: "Gillespie, Gary",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (5*60+30),
                            DurationInMinutes: 170,
                            Timespan: "5:00pm - 7:50pm",
                            Day: "wednesday"
                        }
                    ],
                    "91023": [
                        {
                            Type: "lab",
                            CourseAbbreviation: "CSE 100",
                            Professor: "Gillespie, Gary",
                            Code: "A01",
                            StartTimeInMinutesAfterFirstHour: (1*60+30),
                            DurationInMinutes: 170,
                            Timespan: "9:00am - 11:50am",
                            Day: "monday"
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

// Write your JavaScript Code.
function updateMetadata(metadataList)
{

}

function insertMetadata(metadata)
{

}

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
    var numHalfHourInc = (meeting.StartTimeInMinutesAfterFirstHour) / 30;
    var height30MinInc = document.querySelector(".time").childNodes[1].offsetHeight;
    var timeOffSet = 50;

    var top = (numHalfHourInc * height30MinInc + timeOffSet) + "px";
    var height = ((meeting.DurationInMinutes) * (height30MinInc) / 30) + "px";

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
    var top = pos['top'];
    var height = pos['height'];
    
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
    icon.id = meeting.Type;
    eventHeader.append(icon);

    /* create an icon label and add to icon div */
    var iconLabel = document.createElement('div');
    iconLabel.className = "class-icon-label";
    iconLabel.innerText = "LE";
    icon.append(iconLabel);

    // Use first two letters as text
    iconLabel.innerText = meeting.Type.toUpperCase().substr(0,2);

    /* create an event info div */
    var eventInfo = document.createElement('div');
    eventInfo.className = "event-info";

    /** create spans from meeting object **/
    /* CourseAbbreviation */
    var classSpan = document.createElement('span');
    classSpan.innerHTML = meeting.CourseAbbreviation;

    /* Professor */
    var profSpan = document.createElement('span');
    profSpan.innerHTML = meeting.Professor;

    /* time range */
    var timeSpan = document.createElement('span');
    timeSpan.innerHTML = meeting.Timespan;

    /* section Code */
    var sectSpan = document.createElement('span');
    sectSpan.innerHTML = meeting.Code;

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
        var selectedBase = courses[meeting]["selectedBase"]
        var selectedSection = courses[meeting]["selectedSection"]

        /* get list of selected base (i.e. lectures) and section elements (i.e. discussions) */
        var baseElements = courses[meeting]["bases"][selectedBase]["baseElements"];
        var sectionElements = courses[meeting]["bases"][selectedBase]["sectionElements"][selectedSection];

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