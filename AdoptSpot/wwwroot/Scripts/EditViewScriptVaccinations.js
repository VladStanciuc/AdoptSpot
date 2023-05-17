

$(document).ready(function () {
    $('#delete-images-button').click(function (event) {
        event.preventDefault();

        var imageIds = $('.image-checkbox:checked').map(function () {
            return $(this).val();
        }).get();

        var url = $(this).data('delete-images-url');
        console.log(url);

        $.ajax({
            url: url,
            method: 'DELETE',
            contentType: "application/json",
            data: JSON.stringify(imageIds),
            success: function (response) {
                if (response.success) {
                    // Refresh the page or remove the deleted images from the DOM
                    location.reload();
                }
            },
            error: function (request, status, error) {
                console.log("Error. Request:", JSON.stringify(request));
            }
        });
    });
});

function addVaccination(vaccinationsTableBody) {

    console.log("Adding a new vaccination...")
    var vaccinationsTableBody = document.getElementById("vaccinationsTableBody");
    var index = vaccinationsTableBody.children.length;
    var newRow = `
    <tr data-id="new">
        <td><input name="MedicalRecord.Vaccines[${index}].Disease" class="form-control" readonly /></td>
        <td><input id="dateAdministered_new" name="MedicalRecord.Vaccines[${index}].DateAdministered" type="date" class="form-control" onblur="checkDates('new')" readonly /></td>
        <td><input name="MedicalRecord.Vaccines[${index}].VeterinarianName" class="form-control" readonly /></td>
        <td><input id="expirationDate_new" name="MedicalRecord.Vaccines[${index}].ExpirationDate" type="date" class="form-control" onblur="checkDates('new')" readonly /></td>
        <td><input name="MedicalRecord.Vaccines[${index}].BatchNumber" class="form-control" readonly /></td>
        <td><input name="MedicalRecord.Vaccines[${index}].Notes" class="form-control" readonly /></td>
        <td><input name="MedicalRecord.Vaccines[${index}].Id" type="hidden" value=${index} /></td>
        <td>
            <a href="#" class="btn btn-secondary btn-sm" onclick="toggleReadOnly('new'); return false">Edit</a>
            <a href="#" class="btn btn-danger btn-sm" onclick="deleteVaccination( 'new'); return false;">Delete</a>
        </td>

    </tr>`;
    vaccinationsTableBody.insertAdjacentHTML("beforeend", newRow);

}

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("addVaccinationBtn").addEventListener("click", addVaccination);
});

function checkDates(index) {
    var dateAdministeredInput = document.getElementById('dateAdministered_' + index);
    var expirationDateInput = document.getElementById('expirationDate_' + index);

    var dateAdministered = new Date(dateAdministeredInput.value);
    var expirationDate = new Date(expirationDateInput.value);

    if (dateAdministered > expirationDate) {
        alert('Date Administered cannot be later than Expiration Date.');
        expirationDateInput.value = dateAdministeredInput.value;
    }
}

function deleteVaccination(vaccineId) {

    if (vaccineId === 'new') {
        // Get the table body
        var vaccinationsTableBody = document.getElementById("vaccinationsTableBody");

        // Select the row with the specific data-id and remove it
        var rowToDelete = vaccinationsTableBody.querySelector(`tr[data-id="${vaccineId}"]`);
        rowToDelete.remove();
    } else {
        // Perform an AJAX call to delete the vaccination
        fetch(`/Pets/EditVaccinationDelete/${vaccineId}`, {
            method: "DELETE"
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    // Get the table body
                    var vaccinationsTableBody = document.getElementById("vaccinationsTableBody");

                    // Select the row with the specific data-id and remove it
                    var rowToDelete = vaccinationsTableBody.querySelector(`tr[data-id="${vaccineId}"]`);
                    rowToDelete.remove();

                    // Add the removed vaccination ID to the form
                    var removedVaccinations = document.getElementById("removedVaccinations");
                    var input = document.createElement("input");
                    input.type = "hidden";
                    input.name = "removedVaccinations[]";
                    input.value = vaccineId;
                    removedVaccinations.appendChild(input);
                } else {
                    alert("Error deleting the vaccination.");
                }
            })
            .catch(error => {
                console.error("Error:", error);
                alert("Error deleting the vaccination.");
            });
    }

}
async function updateVaccinations(petId, updatedVaccinations) {
    const response = await fetch(`/Pets/EditVaccinationUpdate/${petId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify(updatedVaccinations)
    });

    if (!response.ok) {
        console.error('Error updating vaccinations', response);
    } else {
        console.log('Vaccinations updated successfully');
    }
}


function toggleReadOnly(vaccineId) {
    var vaccinationsTableBody = document.getElementById("vaccinationsTableBody");
    var row = vaccinationsTableBody.querySelector(`tr[data-id="${vaccineId}"]`);
    if (row) {
        var inputs = row.querySelectorAll("input");

        for (var i = 0; i < inputs.length; i++) {
            var input = inputs[i];
            if (input.hasAttribute("readonly")) {
                input.removeAttribute("readonly");
            } else {
                input.setAttribute("readonly", "");
            }
        }
    } else {
        console.error("Error: Row not found for vaccineId: ", vaccineId);
    }
}

async function submitFormWithAddedVaccinations(petId) {
    
    const addedVaccinations = document.querySelectorAll('tr[data-id="new"]');
    const form = document.getElementById('editForm');

    for (const vaccinationRow of addedVaccinations) {
        const vaccination = {
            Disease: vaccinationRow.querySelector('input[name*="Disease"]').value,
            DateAdministered: vaccinationRow.querySelector('input[name*="DateAdministered"]').value,
            VeterinarianName: vaccinationRow.querySelector('input[name*="VeterinarianName"]').value,
            ExpirationDate: vaccinationRow.querySelector('input[name*="ExpirationDate"]').value,
            BatchNumber: vaccinationRow.querySelector('input[name*="BatchNumber"]').value,
            Notes: vaccinationRow.querySelector('input[name*="Notes"]').value
        };

        console.log(JSON.stringify(vaccination));

        const response = await fetch(`/Pets/EditVaccination/${petId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify(vaccination)
        });

        if (!response.ok) {
            console.error('Error adding vaccination', response);
            return;
        }
       
    }

    // Create a FormData object from the form
    let formData = new FormData(form);
    
    // Perform an AJAX request to send the form data to the server
    const response = await fetch(form.action, {
        method: 'POST',
        body: formData,
    });

    if (!response.ok) {
        console.error('Error submitting form', response);
        console.error(`Error submitting form, status: ${response.status}, status text: ${response.statusText}`);
    } else {
        console.log('Form submitted successfully');
        // refresh the page
        window.location.reload();
    }
}
