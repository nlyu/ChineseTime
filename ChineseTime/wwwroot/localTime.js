// Code learnt and get from Gérald Barré, link: https://www.meziantou.net/convert-datetime-to-user-s-time-zone-with-serverdsaf-side-blazor.htm

function blazorGetTimezoneOffset() {
    return new Date().getTimezoneOffset();
}