// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function alertDbSave(success) {
    if (success === 1) {
        alert("Record updated successfully");
    }
}

function alertDbInserted(success) {
    if (success === 1) {
        alert("Record inserted successfully");
    }
}

function alertDbDeleted(success) {
    if (success === 1) {
        alert("Record deleted successfully");
    }
}
