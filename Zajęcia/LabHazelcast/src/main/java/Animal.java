import java.io.Serializable;

public class Animal implements Serializable {

	private static final long serialVersionUID = 1L;

	public String name;
	public int birthyear;
	public String comments;
	public int catwalk;
	public String breed;
	public String species;


	public Animal(String name, int birthyear, String comments, int catwalk, String breed, String species)
	{
		this.name = name;
		this.birthyear = birthyear;
		this.comments = comments;
		this.catwalk = catwalk;
		this.breed = breed;
		this.species = species;

	}



	@Override
	public String toString(){
		return "Zwierze " + name + " urodzone w " + birthyear + ", Uwagi: " + comments + ", nr wybiegu: " + catwalk  + ", gatunek: " + species + ", rasa: " + breed;
	}


}
