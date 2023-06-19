using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Linq;

namespace SimpleCalendar
{
    public class CalendarInterface : MonoBehaviour
    {
        [SerializeField] UIDocument calendarDocument;
        [SerializeField] int year, month, today; //TODO: Delete, for testing purposes
        readonly string CURRENT_DAY_CLASS = "current-day";
        readonly string WEEKEND_DAY_CLASS = "weekend-day";
        Label monthLabel, yearLabel, leftArrow, rightArrow;
        VisualElement rootElement, calendarBody;
        int currentMonth;

        public void ShowCalendar()
        {
            rootElement?.SetEnabled(true);
            FillCalendar();
        }

        public void HideCalendar()
        {
            ClearList();
            rootElement?.SetEnabled(false);
        }

        private void Start()
        {
            if (!Validation.ValidateRootElement(calendarDocument, this.name))
                return;

            currentMonth = month;

            rootElement = calendarDocument.rootVisualElement;
            calendarBody = rootElement.Q<VisualElement>("calendar-body");
            monthLabel = rootElement.Q<Label>("month-label");
            yearLabel = rootElement.Q<Label>("year-label");

            leftArrow = rootElement.Q<Label>("prev-month");
            rightArrow = rootElement.Q<Label>("next-month");
            rightArrow.RegisterCallback<MouseDownEvent>(SetNextMonth);
            leftArrow.RegisterCallback<MouseDownEvent>(SetPreviousMonth);

            FillCalendar();
        }

        private void FillCalendar()
        {
            monthLabel.text = new DateTime(year, currentMonth, 1).ToString("MMMM");
            yearLabel.text = year.ToString();
            SetMonthDays();
        }

        private void ClearList()
        {
            var rows = calendarBody.Children().ToList();

            foreach (var row in rows)
            {
                var days = row.Children().Cast<Label>().ToList();

                foreach (var day in days)
                {
                    if (!day.text.IsEmpty())
                    {
                        day.text = "";
                        day.RemoveFromClassList(CURRENT_DAY_CLASS);
                        day.RemoveFromClassList(WEEKEND_DAY_CLASS);
                        day.UnregisterCallback<MouseDownEvent>(ShowPopUp);
                    }
                }
            }
        }

        private void SetMonthDays()
        {
            var rows = calendarBody.Children().ToList();
            var dayCounter = 1;
            var date = new DateTime(year, currentMonth, dayCounter);
            var maxDays = DateTime.DaysInMonth(year, currentMonth);
            foreach (var row in rows)
            {
                var days = row.Children().Cast<Label>().ToList();
                for (var i = (int)date.DayOfWeek; i < days.Count && dayCounter <= maxDays; i++)
                {
                    var day = days[i];
                    if (dayCounter == today && currentMonth == month)
                        day.AddToClassList(CURRENT_DAY_CLASS);
                    else if (i == (int)DayOfWeek.Sunday || i == (int)DayOfWeek.Saturday)
                        day.AddToClassList(WEEKEND_DAY_CLASS);

                    day.text = dayCounter.ToString();
                    day.RegisterCallback<MouseDownEvent>(ShowPopUp);
                    dayCounter++;
                }
                if (dayCounter <= maxDays)
                    date = new DateTime(year, currentMonth, dayCounter);
            }
        }

        private void SetPreviousMonth(MouseDownEvent evt)
        {
            if (currentMonth == (int)Month.January) return;
            Debug.Log("Previous Month");
            currentMonth--;
            ClearList();
            FillCalendar();
        }

        private void SetNextMonth(MouseDownEvent evt)
        {
            if (currentMonth == (int)Month.December) return;
            Debug.Log("Next Month");
            currentMonth++;
            ClearList();
            FillCalendar();
        }

        //TODO: Create popup when clicking on a day of the month
        private void ShowPopUp(MouseDownEvent evt)
        {
            Debug.Log("Opened pop up");
        }

        private void ClosePopUp()
        {
            Debug.Log("Closed pop up");
        }
    }
}

public enum Month
{
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
}
