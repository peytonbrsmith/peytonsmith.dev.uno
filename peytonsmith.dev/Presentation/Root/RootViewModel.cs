namespace peytonsmith.dev.Presentation;

public class RootViewModel { 
	private readonly INavigator _navigator;

    public RootViewModel(INavigator navigator){
        _navigator = navigator;
    }

}
