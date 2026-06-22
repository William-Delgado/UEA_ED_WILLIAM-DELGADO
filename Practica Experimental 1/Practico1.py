class Contacto:
    def __init__(self, nombre, telefono, correo, direccion):
        self.nombre = nombre
        self.telefono = telefono
        self.correo = correo
        self.direccion = direccion

    def mostrar(self):
        print(f"Nombre: {self.nombre}")
        print(f"Teléfono: {self.telefono}")
        print(f"Correo: {self.correo}")
        print(f"Dirección: {self.direccion}")
        print("-" * 40)


class AgendaTelefonica:
    def __init__(self):
        self.contactos = []

    def agregar_contacto(self):
        print("\n--- AGREGAR CONTACTO ---")
        nombre = input("Ingrese el nombre: ")
        telefono = input("Ingrese el teléfono: ")
        correo = input("Ingrese el correo: ")
        direccion = input("Ingrese la dirección: ")

        contacto = Contacto(nombre, telefono, correo, direccion)
        self.contactos.append(contacto)

        print("Contacto agregado correctamente.")

    def listar_contactos(self):
        print("\n--- LISTA DE CONTACTOS ---")

        if len(self.contactos) == 0:
            print("No existen contactos registrados.")
        else:
            for i, contacto in enumerate(self.contactos, start=1):
                print(f"Contacto N.º {i}")
                contacto.mostrar()

    def buscar_contacto(self):
        print("\n--- BUSCAR CONTACTO ---")
        nombre_buscar = input("Ingrese el nombre a buscar: ")

        encontrado = False

        for contacto in self.contactos:
            if contacto.nombre.lower() == nombre_buscar.lower():
                print("Contacto encontrado:")
                contacto.mostrar()
                encontrado = True

        if not encontrado:
            print("No se encontró ningún contacto con ese nombre.")

    def eliminar_contacto(self):
        print("\n--- ELIMINAR CONTACTO ---")
        nombre_eliminar = input("Ingrese el nombre del contacto a eliminar: ")

        for contacto in self.contactos:
            if contacto.nombre.lower() == nombre_eliminar.lower():
                self.contactos.remove(contacto)
                print("Contacto eliminado correctamente.")
                return

        print("No se encontró el contacto.")

    def reporte_matriz(self):
        print("\n--- REPORTE EN FORMA DE MATRIZ ---")

        if len(self.contactos) == 0:
            print("No existen contactos para generar el reporte.")
            return

        matriz_contactos = []

        for contacto in self.contactos:
            fila = [
                contacto.nombre,
                contacto.telefono,
                contacto.correo,
                contacto.direccion
            ]
            matriz_contactos.append(fila)

        print("Nombre\t\tTeléfono\tCorreo\t\tDirección")
        print("-" * 70)

        for fila in matriz_contactos:
            print(f"{fila[0]}\t\t{fila[1]}\t{fila[2]}\t{fila[3]}")

    def menu(self):
        while True:
            print("\n===== AGENDA TELEFÓNICA =====")
            print("1. Agregar contacto")
            print("2. Listar contactos")
            print("3. Buscar contacto")
            print("4. Eliminar contacto")
            print("5. Reporte en matriz")
            print("6. Salir")

            opcion = input("Seleccione una opción: ")

            if opcion == "1":
                self.agregar_contacto()
            elif opcion == "2":
                self.listar_contactos()
            elif opcion == "3":
                self.buscar_contacto()
            elif opcion == "4":
                self.eliminar_contacto()
            elif opcion == "5":
                self.reporte_matriz()
            elif opcion == "6":
                print("Gracias por utilizar la agenda telefónica.")
                break
            else:
                print("Opción no válida. Intente nuevamente.")

agenda = AgendaTelefonica()
agenda.menu()