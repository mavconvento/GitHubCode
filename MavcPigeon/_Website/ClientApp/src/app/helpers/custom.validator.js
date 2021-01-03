"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ComparePassword = void 0;
// To validate password and confirm password
function ComparePassword(controlName, matchingControlName) {
    return function (formGroup) {
        var control = formGroup.controls[controlName];
        var matchingControl = formGroup.controls[matchingControlName];
        if (matchingControl.errors && !matchingControl.errors.mustMatch) {
            return;
        }
        if (control.value !== matchingControl.value) {
            matchingControl.setErrors({ mustMatch: true });
        }
        else {
            matchingControl.setErrors(null);
        }
    };
}
exports.ComparePassword = ComparePassword;
//# sourceMappingURL=custom.validator.js.map