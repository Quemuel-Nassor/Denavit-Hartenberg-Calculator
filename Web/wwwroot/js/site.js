// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Function to copy data to clipboard
function copyToClipboard(id) {
    var field = $(id);
    if (field != null && field.length > 0) {
        field.select();
        document.execCommand('copy');
    }
};

//Function to clear input fields
$(function () {
    $('.ClearValue').prop('value', null);
    $('.ClearValue').mask('##.#####', { reverse: true });
});

//Function to add a new joint
function addJoint() {
    var joints = $('#joints');
    var len = parseInt(joints.children().length) - 1;

    var id = joints[0].children[len].id
    id = parseInt(id.substring(6)) + 1;

    $(
        '<div id="joint-' + id + '" class="col-md-12 px-0">' +
        '<div class="row justify-content-between">' +
        '<div class="col-md-auto">' +
        '<div class="form-group">' +
        '<label class="control-label" for="Joints_' + id + '__Theta">Angle <i class="theta"></i></label>' +
        '<input type="text" required class="ClearFields form-control input-validation-error" data-val="true" data-val-number="The field Theta must be a number." data-val-required="Theta is required" id="Joints_' + id + '__Theta" name="Joints[' + id + '].Theta" value="" aria-describedby="Joints_' + id + '__Theta-error" aria-invalid="true">' +
        '<span class="text-danger field-validation-error" data-valmsg-for="Joints[' + id + '].Theta" data-valmsg-replace="true">' +
        '</div>' +
        '</div>' +
        '<div class="col-md-auto">' +
        '<div class="form-group">' +
        '<label class="control-label" for="Joints_' + id + '__DistanceD">Distance <i class="d-ch"></i></label>' +
        '<input type="text" required class="ClearFields form-control input-validation-error" data-val="true" data-val-number="The field DistanceD must be a number." data-val-required="DistanceD is required" id="Joints_' + id + '__DistanceD" name="Joints[' + id + '].DistanceD" value="" aria-describedby="Joints_' + id + '__DistanceD-error" aria-invalid="true">' +
        '<span class="text-danger field-validation-error" data-valmsg-for="Joints[' + id + '].DistanceD" data-valmsg-replace="true">' +
        '</div>' +
        '</div>' +
        '<div class="col-md-auto">' +
        '<div class="form-group">' +
        '<label class="control-label" for="Joints_' + id + '__DistanceA">Distance <i class="a-ch"></i></label>' +
        '<input type="text" required class="ClearFields form-control input-validation-error" data-val="true" data-val-number="The field DistanceA must be a number." data-val-required="DistanceA is required" id="Joints_' + id + '__DistanceA" name="Joints[' + id + '].DistanceA" value="" aria-describedby="Joints_' + id + '__DistanceA-error" aria-invalid="true">' +
        '<span class="text-danger field-validation-error" data-valmsg-for="Joints[' + id + '].DistanceA" data-valmsg-replace="true">' +
        '</div>' +
        '</div>' +
        '<div class="col-md-auto">' +
        '<div class="form-group">' +
        '<label class="control-label" for="Joints_' + id + '__Alpha">Angle <i class="alpha"></i></label>' +
        '<input type="text" required class="ClearFields form-control input-validation-error" data-val="true" data-val-number="The field Alpha must be a number." data-val-required="Alpha is required" id="Joints_' + id + '__Alpha" name="Joints[' + id + '].Alpha" value="" aria-describedby="Joints_' + id + '__Alpha-error" aria-invalid="true">' +
        '<span class="text-danger field-validation-error" data-valmsg-for="Joints[' + id + '].Alpha" data-valmsg-replace="true">' +
        '</div>' +
        '</div>' +
        '<div class="col-md-auto">' +
        '<div class="form-group pt-2">' +
        '<label></label>' +
        '<div class="row m-0">' +
        '<button type="button" onclick="removeJoint(' + id + ')" class="RemoveBtn btn-block btn btn-danger" disabled="">' +
        '<span>Remove</span>' +
        '</button>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>'
    ).appendTo(joints);
    enableOrDisableDeleteBtn();
};

//Function to remove joint
function removeJoint(index) {
    var joints = $('#joints');

    if (index == 0 && joints.children().length == 1) {
        alert("It is necessary to inform the parameters of at least one joint!");
        return;
    }

    var len = parseInt(joints.children().length) - 1;
    var lastId = joints[0].children[len].id
    lastId = parseInt(lastId.substring(6));

    for (var i = index; i < lastId; i++) {

        var DistanceA = $('#Joints_' + i + '__DistanceA')[0];
        var DistanceD = $('#Joints_' + i + '__DistanceD')[0];
        var Alpha = $('#Joints_' + i + '__Alpha')[0];
        var Theta = $('#Joints_' + i + '__Theta')[0];

        var j = parseInt(i) + 1;

        DistanceA.value = $('#Joints_' + j + '__DistanceA')[0].value;
        DistanceD.value = $('#Joints_' + j + '__DistanceD')[0].value;
        Alpha.value = $('#Joints_' + j + '__Alpha')[0].value;
        Theta.value = $('#Joints_' + j + '__Theta')[0].value;
    }

    var child = joints.find('#joint-' + lastId);
    child.remove();
    enableOrDisableDeleteBtn();
};

//Function to enable or disable delete button
function enableOrDisableDeleteBtn() {
    var joints = $('#joints');
    if (joints.children().length > 1) {
        $('.RemoveBtn').prop('disabled', false);
    } else {
        $('.RemoveBtn').prop('disabled', true);
    }
};