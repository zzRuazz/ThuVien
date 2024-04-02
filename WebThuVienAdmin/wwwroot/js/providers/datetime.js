class DateTimeProvider {
    //dd/MM/yyyy(client) | dd/MM/yyyy HH:mm:ss(client) -> yyyy-MM-dd HH:mm:ss(server)
    static ConvertToServerDateTime(strclientDateTime, seperate) {
        if (seperate === undefined)
            seperate = "/";
        let [day, month, year] = strclientDateTime.split(seperate);

        if (strclientDateTime.indexOf(":") !== -1) {
            let yearSplit = year.split(' ');
            year = yearSplit[0];

            let [hour, minute, second] = yearSplit[1].split(':');

            return parseInt(year) + "-" + parseInt(month) + "-" + parseInt(day) + ' '
                + parseInt(hour) + ':' + parseInt(minute) + (Number.isNaN(parseInt(second)) ? '' : (':' + + parseInt(second)));
        }

        return parseInt(year) + "-" + parseInt(month) + "-" + parseInt(day);
    }

    //yyyy-MM-ddTHH:mm:ss.i -> HH:mm:ss dd/MM/yyyy
    static ConvertDBTimeToClientFormat2(strServerDateTime) {
        if (strServerDateTime) {
            let [date, time] = strServerDateTime.split(('T'));
            let [year, month, day] = date.split('-');
            let [hour, minute, second] = time.split(':');
            let hourString = parseInt(hour) >= 10 ? parseInt(hour) : "0" + parseInt(hour);
            let minuteString = parseInt(minute) >= 10 ? parseInt(minute) : "0" + parseInt(minute);
            let secondString = parseInt(second) >= 10 ? parseInt(second) : "0" + parseInt(second);

            return year + '-' + month + '-' + day + ' ' + hourString + ':' + minuteString + (Number.isNaN(parseInt(second)) ? '' : (':' + secondString));
        } else {
            return '';
        }
    }

    //yyyy-MM-ddTHH:mm:ss.i -> HH:mm:ss dd/MM/yyyy
    static ConvertDBTimeToClientFormat(strServerDateTime) {
        if (strServerDateTime) {
            let [date, time] = strServerDateTime.split(('T'));
            let [year, month, day] = date.split('-');
            let [hour, minute, second] = time.split(':');
            let hourString = parseInt(hour) >= 10 ? parseInt(hour) : "0" + parseInt(hour);
            let minuteString = parseInt(minute) >= 10 ? parseInt(minute) : "0" + parseInt(minute);
            let secondString = parseInt(second) >= 10 ? parseInt(second) : "0" + parseInt(second);

            return hourString + ':' + minuteString + (Number.isNaN(parseInt(second)) ? '' : (':' + secondString)) + ' ' + day + "/" + month + "/" + year;
        } else {
            return '';
        }
    }

    //yyyy-MM-ddTHH:mm:ss.i -> dd-MM-yyyy
    static ConvertDBTimeToDateFormatOnly(strServerDateTime) {
        if (strServerDateTime) {
            let [date, time] = strServerDateTime.split(('T'));
            return date;
        } else {
            return '';
        }
    }

    // yyyy-mm-dd => dd/mm/yyyy
    static ConvertToClientFormat(strclientDateTime, seperate) {
        if (seperate === undefined)
            seperate = "/";
        let [year, month, day] = strclientDateTime.split(seperate);

        if (strclientDateTime.indexOf(":") !== -1) {
            let yearSplit = year.split(' ');
            year = yearSplit[0];

            let [hour, minute, second] = yearSplit[1].split(':');

            return parseInt(day) + "/" + parseInt(month) + "/" + parseInt(year) + ' '
                + parseInt(hour) + ':' + parseInt(minute) + (Number.isNaN(parseInt(second)) ? '' : (':' + + parseInt(second)));
        }

        return parseInt(day) + "/" + parseInt(month) + "/" + parseInt(year);
    }

    static ValidateDateTime(strClientDateTime, seperate) {
        strClientDateTime = strClientDateTime.trim();

        if (seperate === undefined)
            seperate = "/";
        let [day, month, year] = strClientDateTime.split(seperate);

        day = parseInt(day);
        if (isNaN(day) || day > 31 || day < 0)
            return false;

        month = parseInt(month);
        if (isNaN(month) || month > 12 || month < 0)
            return false;

        if (month === 2 && day > 29)
            return false;

        if (day === 31 && [2, 4, 6, 9, 11].indexOf(month) > -1)
            return false;

        let yearSplit = year.split(' ');
        year = parseInt(yearSplit[0]);
        if (isNaN(year) || year < 1700 || year < 0)
            return false;

        let time = yearSplit[1];
        if (time === '' || time === null || time === undefined)
            return true;

        if (time.indexOf(":") === -1)
            return false;

        //let [hour, minute, second] = time.split(':');
        let [hour, minute] = time.split(':');
        hour = parseInt(hour);
        if (isNaN(hour) || hour > 23 || hour < 0)
            return false;

        minute = parseInt(minute);
        if (isNaN(minute) || minute > 59 || minute < 0)
            return false;

        //second = parseInt(second);
        //if (isNaN(second) || second > 59 || second < 0)
        //    return false;

        return true;
    }

    // /Date(1538931600000)/ -> dd/mm/yyyy
    static ConvertToClient(strDate, seperate, time) {
        if (strDate != null && strDate.length > 0) {
            let date = new Date(strDate.match(/\d+/).map(Number)[0]);
            let day = date.getDate();
            if (day.toString().length === 1)
                day = "0" + day;
            let month = date.getMonth() + 1;
            if (month.toString().length === 1)
                month = "0" + month;
            let year = date.getFullYear();
            if (seperate === undefined)
                seperate = "/";
            if (time !== undefined) {
                if (time === true) {
                    let hour = date.getHours();
                    if (hour.toString().length == 1)
                        hour = "0" + hour;
                    let minute = date.getMinutes();
                    if (minute.toString().length == 1)
                        minute = "0" + minute;
                    let second = date.getSeconds();
                    if (second.toString().length == 1)
                        second = "0" + second;
                    return day + seperate + month + seperate + year + " " + hour + ":" + minute + ":" + second;
                }
            }
            return day + seperate + month + seperate + year;
        }
    }

    static ConvertToClientDate(strDate) {
        let date = new Date(strDate.match(/\d+/).map(Number)[0]);
        return date;
    }

    static SubDateTime(startDate, endDate) {
        return new Date(endDate.match(/\d+/).map(Number)[0]) - new Date(startDate.match(/\d+/).map(Number)[0]);
    }

    static Inits() {
        $('[data-type="date"].init').each(function () {
            let $control = $(this);
            $control.removeClass("init");
            $control.datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy',
                todayBtn: true
            });
        });
    }
}

$(function () {
    DateTimeProvider.Inits();

});

$(document).ajaxComplete(function () {
    DateTimeProvider.Inits();
});