// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// TODO: Optimize Duplication of functionality from TimestampFormatUtility form Web project
const timestampFormatUtility = (() => {
    const mapTimestamp = (date) => {
        const targetDate = moment(date).locale('bg');
        const currentDate = moment().locale('bg');
        const totalDaysDiff = currentDate.diff(targetDate, 'days');
        const totalHoursDiff = currentDate.diff(targetDate, 'hours');
        const totalMinutesDiff = currentDate.diff(targetDate, 'minutes');
        const totalSecondsDiff = currentDate.diff(targetDate, 'seconds');

        if (totalDaysDiff > 730) return "on " + targetDate.format("dd/MM/yyyy");
        if (totalDaysDiff > 365 && totalDaysDiff <= 730) return "1 year and " + (((currentDate.month() - targetDate.month()) + 12 * (currentDate.year() - targetDate.year())) - 12);
        if (totalDaysDiff > 30 && totalDaysDiff <= 365) return (currentDate.month() - targetDate.month()) + 12 * (currentDate.year() - targetDate.year()) + " months ago";
        if (totalDaysDiff > 7 && totalDaysDiff <= 30) return (totalDaysDiff / 7) + " weeks ago";
        if (totalDaysDiff > 1 && totalDaysDiff <= 7) return totalDaysDiff + " days ago";
        if (totalDaysDiff < 1 && totalMinutesDiff > 60) return totalHoursDiff + " hours ago";
        if (totalMinutesDiff >= 1 && totalMinutesDiff <= 60) return totalMinutesDiff + " minutes ago";
        if (totalSecondsDiff >= 0 && totalMinutesDiff < 1) return totalSecondsDiff + " seconds ago";

        throw Error("Invalid timestamp provided...");
    };

    return {
        mapTimestamp
    }
})();