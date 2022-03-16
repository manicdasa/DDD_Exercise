"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ErrorHandler = void 0;
var ErrorHandler = function (e, text, alert) {
    if (e != undefined && e.response != undefined && e.response.data != undefined && e.response.data.message != undefined) {
        alert.error(e.response.data.message);
    }
    else
        alert.error(text);
};
exports.ErrorHandler = ErrorHandler;
//# sourceMappingURL=ErrorServices.js.map