
    
        $(function () {

            $("#medicalTreatmentTableBody .edit-button").on("click", function () {
                var row = $(this).closest("tr");
                $("td", row).each(function (i) {
                    if (i < 8) { // Exclude last cell with buttons
                        var span = $(this).find("span");
                        var input = $(this).find("input");
                        input.width(input.val().length *11);
                        if (span.length > 0) {
                            span.hide();
                            input.show();
                        } else {
                            var text = $(this).text();
                            $(this).html('<span>' + text + '</span><input type="text" value="' + text + '" style="display: none" />');
                            $(this).find("span").hide();
                            $(this).find("input").show();
                        }
                        $(this).css("white-space", "nowrap");
                    }
                });
                row.find(".update-button").show().css("display", "inline-block");;
                row.find(".cancel-button").show().css("display", "inline-block");;
                $(this).hide();
            });

            $("#medicalTreatmentTableBody .update-button").on("click", function () {
                var row = $(this).closest("tr");
                var treatment = {};
                var petId = $("#petId").val();
                treatment.Id = $(this).data('id');
                $("td", row).each(function (i) {
                    if (i < 8) {
                        var span = $(this).find("span");
                        var input = $(this).find("input");
                        span.html(input.val());
                        span.show();
                        input.hide();
                        treatment[$(this).data('field')] = input.val();
                    }
                });

                row.find(".edit-button").show();
                row.find(".cancel-button").hide();
                $(this).hide();

                $.ajax({
                    type: "POST",
                    url: "UpdateMedicalTreatment/" + petId ,
                    data: JSON.stringify(treatment),
                    contentType: "application/json", 
                    dataType: "json"
                });
            });

            $("#medicalTreatmentTableBody .cancel-button").on("click", function () {
                var row = $(this).closest("tr");
                $("td", row).each(function (i) {
                    if (i < 8) {
                        var span = $(this).find("span");
                        var input = $(this).find("input");
                        input.val(span.html());
                        span.show();
                        input.hide();
                    }
                });
                row.find(".edit-button").show();
                row.find(".update-button").hide();
                $(this).hide();
            });
        });

        $("#medicalTreatmentTableBody .delete-button").on("click", function () {
            var id = $(this).data("id");
            var petId = $("#petId").val();
            var row = $(this).closest("tr"); // Store the row
            if (confirm('Are you sure to delete this record?')) {
                $.ajax({
                    url: "DeleteMedicalTreatment/" + petId + "/" + id,
                    type: 'DELETE',
                    success: function (response) {
                        if (response.status === "success") {
                            // remove row from table
                            row.remove(); // Use the stored row here
                        } else {
                            alert('Error occurred while deleting the treatment.')
                        }
                    },
                    error: function (err) {
                        alert('Error occurred while deleting the treatment.')
                    }
                });
            }
        });

        $(".add-button").on("click", function () {
            // Define the row HTML
            var rowHtml = `
        <tr>
            <td class="TreatmentDescription" data-field="TreatmentDescription">
                <span></span>
                <input type="text" style="display: block" />
            </td>
            <td class="PrescribingVeterinarian" data-field="PrescribingVeterinarian">
                <span></span>
                <input type="text" style="display: block" />
            </td>
            <td class="Cost" data-field="Cost">
                <span></span>
                <input type="text" style="display: block" />
            </td>
            <td class="Diagnosis" data-field="Diagnosis">
                <span></span>
                <input type="text" style="display: block" />
            </td>
            <td class="Medication" data-field="Medication">
                <span></span>
                <input type="text" style="display: block" />
            </td>
            <td class="DosageAndUnit" data-field="DosageAndUnit">
                <span></span>
                <input type="text" style="display: block" />
            </td>
            <td class="StartDate" data-field="StartDate">
                <span></span>
                <input type="date" style="display: block" />
            </td>
            <td class="EndDate" data-field="EndDate">
                <span></span>
                <input type="date" style="display: block" />
            </td>
            <td>
                <button type="button" class="btn btn-success btn-sm save-new-treatment-button">Save</button>
                <button type="button" class="btn btn-warning btn-sm cancel-new-treatment-button">Cancel</button>
            </td>
        </tr>`;
            // Add the row to the table body
            $("#medicalTreatmentTableBody").prepend(rowHtml);
        });


        
        $("#medicalTreatmentTableBody").on("click", ".save-new-treatment-button", function () {
            var row = $(this).closest("tr");
            var petId = $("#petId").val();

            var treatmentData = {
                TreatmentDescription: row.find("td.TreatmentDescription input").val(),
                PrescribingVeterinarian: row.find("td.PrescribingVeterinarian input").val(),
                Cost: row.find("td.Cost input").val(),
                Diagnosis: row.find("td.Diagnosis input").val(),
                Medication: row.find("td.Medication input").val(),
                DosageAndUnit: row.find("td.DosageAndUnit input").val(),
                StartDate: row.find("td.StartDate input").val(),
                EndDate: row.find("td.EndDate input").val()
            };

            $.ajax({
                url: "AddMedicalTreatment/" + petId,
                type: 'POST',
                data: JSON.stringify(treatmentData),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (response.status === "success") {
                        console.log('AJAX call succeeded'); // Add this line
                        // convert row to non-editable
                        row.find("span").each(function () {
                            $(this).text($(this).next("input").val());
                        });
                        row.find("input").hide();
                        row.find(".save-new-treatment-button").remove();
                        row.find(".cancel-new-treatment-button").remove();
                        row.append('<button type="button" class="btn btn-primary btn-sm edit-button">Edit</button><button type="button" class="btn btn-danger btn-sm delete-button">Delete</button>');
                    } else {
                        alert('Error occurred while adding the treatment.');
                    }
                },
                error: function (err) {
                    alert('Error occurred while adding the treatment.');
                }
            });
        });

        $("#medicalTreatmentTableBody").on("click", ".cancel-new-treatment-button", function () {
            $(this).closest("tr").remove();
        });

