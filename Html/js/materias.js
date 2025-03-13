addMateria = async (datos) => {
	const settings = {
		method: 'POST',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
		body: JSON.stringify(datos)
	};
	try {
		const fetchResponse = await fetch(`https://lmazfunction.azurewebsites.net/api/materias`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
}

getMaterias = async () => {
	const settings = {
		method: 'GET',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
	};
	try {
		const fetchResponse = await fetch(`https://lmazfunction.azurewebsites.net/api/materias`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
}

enlistarMaterias = async () => {
	let materias = (await getMaterias()).Value;

	for (var i = 0; i < materias.length; i++) {
		$('#materias').append('<tr><td>' + materias[i].Nombre + '</td><td>' + materias[i].Descripcion + '</td></tr>');
	};
}

agregarMateria = async () => {
	let datos = {
		"Nombre": $('#nombre').val(),
		"Descripcion": $('#descripcion').val(),
	};

	let result = (await addMateria(datos)).Value;

	if (result > 0) {
		alert('Materia agregada');
		$("#modalMateria").modal('toggle');
		$('#nombre').val('');
		$('#descripcion').val('');
		$('#materias').empty();
		enlistarMaterias();
	} else {
		alert('Error al agregar Materia');
	}
}