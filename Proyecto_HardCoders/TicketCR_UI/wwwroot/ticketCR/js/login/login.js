function LoginView() {
    this.Validate = function () {
        var div = $('#validations');

        if (div.text() !== "") {

            messageError('Error', 'Usuario y/o contraseña incorrecta. Inténtalo nuevamente.');

        }

    }
}
$(document).ready(function () {
    var view = new LoginView();
    view.Validate();

});