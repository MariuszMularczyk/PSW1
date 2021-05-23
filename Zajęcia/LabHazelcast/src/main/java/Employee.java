import java.io.Serializable;

public class Employee implements Serializable {

	private static final long serialVersionUID = 1L;

	private String name;
	private int birthyear;
	private String specialization;
	private float salary;

	public Employee(String name, int birthyear, String specialization , float salary)
	{
		this.name = name;
		this.birthyear = birthyear;
		this.specialization = specialization;
		this.salary = salary;
	}


	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public int getBirthyear() {
		return birthyear;
	}

	public void setBirthyear(int birthyear) {
		this.birthyear = birthyear;
	}

	@Override
	public String toString(){
		return "Employee " + name + " born in " + birthyear + ", specialization:  " + specialization + ", Salary: " + salary;
	}

	public float getSalary() {
		return salary;
	}

	public void setSalary(float salary) {
		this.salary = salary;
	}

	public String getSpecialization() {
		return specialization;
	}

	public void setSpecialization(String specialization) {
		this.specialization = specialization;
	}
}