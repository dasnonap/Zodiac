using System.Text.RegularExpressions;

namespace API.Validators{

    public class Validator
    {
        private string variable;    
        private int integerVariable;    

        private float floatVariable;

        // Stirng Constructor
        public Validator( string var )
        {
            variable = var;
        }
        
        // Int Constructor
        public Validator( int integerVar )
        {
            integerVariable = integerVar;
        }

        // Float Constructor
        public Validator( float floatVar )
        {
            floatVariable = floatVar;
        }

        // Checks if field is empty
        public bool IsValidField(){
            if( variable == "" || variable == null )
            {
                return false;
            }
            
            return true;
        }

        // Checks if variable is valid int
        public bool IsValidInteger(){
            if( integerVariable == 0 )
            {
                return false;
            }
            
            return true;
        }

        // Checks if variable is valid float
        public bool IsValidFloat(){
            if( floatVariable == 0 )
            {
                return false;
            }
            
            return true;
        }
        
        // Checks if variable is Valid Email
        public bool IsValidEmail()
        {
            if( ! IsValidField() )
            {
                return false;
            }
            
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            
            if( ! regex.Match( variable ).Success )
            {
                return false;
            }
            
            return true;
        }
        
        // Checks if variable is valid Phone
        public bool IsValidPhone()
        {
            if( ! IsValidField() )
            {
                return false;
            }
            
            Regex regex = new Regex( @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})" );
            
            if( ! regex.Match( variable ).Success )
            {
                return false;
            }
            
            return true;
        }
    }
}