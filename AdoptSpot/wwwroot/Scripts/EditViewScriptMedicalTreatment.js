function toggleReadOnly(treatmentId) {
    var treatmentsTableBody = document.getElementById("treatmentsTableBody");
    var row = treatmentsTableBody.querySelector(`tr[data-id="${treatmentId}"]`);
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
        console.error("Error: Row not found for treatmentId: ", treatmentId);
    }
}

async function submitFormWithAddedTreatments(petId) {

    const addedTreatments = document.querySelectorAll('tr[data-id="new"]');
    const form = document.getElementById('editForm');

    for (const treatmentRow of addedTreatments) {
        const treatment = {
            TreatmentDate: treatmentRow.querySelector('input[name*="TreatmentDate"]').value,
            TreatmentDescription: treatmentRow.querySelector('input[name*="TreatmentDescription"]').value,
            PrescribingVeterinarian: treatmentRow.querySelector('input[name*="PrescribingVeterinarian"]').value,
            Cost: treatmentRow.querySelector('input[name*="Cost"]').value,
            Diagnosis: treatmentRow.querySelector('input[name*="Diagnosis"]').value,
            Medication: treatmentRow.querySelector('input[name*="Medication"]').value,
            Dosage: treatmentRow.querySelector('input[name*="Dosage"]').value,
            DosageUnit: treatmentRow.querySelector('input[name*="DosageUnit"]').value,
            Frequency: treatmentRow.querySelector('input[name*="Frequency"]').value,
            FrequencyUnit: treatmentRow.querySelector('input[name*="FrequencyUnit"]').value,
            StartDate: treatmentRow.querySelector('input[name*="StartDate"]').value,
            EndDate: treatmentRow.querySelector('input[name*="EndDate"]').value,
            Notes: treatmentRow.querySelector('input[name*="Notes"]').value
        };

        console.log(JSON.stringify(treatment));

        const response = await fetch(`/Pets/EditTreatment/${petId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify(treatment)
        });

        if (!response.ok) {
            console.error('Error adding treatment', response);
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
