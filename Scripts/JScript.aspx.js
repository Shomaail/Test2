function limitCharsLength(Object, MaxLen) {
    if (Object.value.length > MaxLen - 1) {
        Object.value = Object.value.substring(0, MaxLen);
    }
}
function limitClipBoard(Object, MaxLen) {
    if (window.clipboardData != null) {
        //alert("clipboardData  is not null");
        if (window.clipboardData.getData("Text").length > MaxLen - 1 - Object.value.length) {
          //  alert("clipboardData  is very big");
            window.clipboardData.setData("Text", window.clipboardData.getData("Text").toString().substring(0, (MaxLen - Object.value.length)));
        }
    }
}
function confirm_delete() {

    if (confirm("Are you sure you want to delete the contact?") == true)

        return true;

    else

        return false;

}
function ValidateTextLength(oSrc, args) {
    args.IsValid = args.Value.length > 10;
}
