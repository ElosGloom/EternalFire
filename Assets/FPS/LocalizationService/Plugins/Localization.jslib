mergeInto(LibraryManager.library, {

 GetSystemLanguageJS: function () {
  var returnStr = ysdk.environment.i18n.lang;  //for Yandex
  //var returnStr = navigator.language;    //for other
  var bufferSize = lengthBytesUTF8(returnStr) + 1;
  var buffer = _malloc(bufferSize);
  stringToUTF8(returnStr, buffer, bufferSize);
  return buffer;
 }
 
});