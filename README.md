# jsonExector
Executes the api defined in Json format. Define API one time and use JSON to create multiple test cases with validation. Chain the outputs to feed in to the next API call through JSON data.

# Motivation
Write integration tests through configuration instead of code using JSON file format.

# Usage
* Identify the APIs test
* Define JSON with inputs and validations

# Sample
## Simple without using Variables
```json
[
  {
    "Name": "Math.Add",
    "Api": "add",
    "Parameters": {
      "num1": 10,
      "num2": 20
    },
    "Expected": {
      "result": 30
    }
  },
  {
    "Name": "Math.Sub",
    "Api": "sub",
    "Parameters": {
      "num1": 10,
      "num2": 20
    },
    "Expected": {
      "result": -10
    }
  }
]
```
## Using Variables
* Define variables
```json
{
  "mynum1": 33,
  "mynum2": 44,
  "item1": "val1",
  "item2": "val2"
}

```
* Use variables
```json
[
  {
    "Name": "Math.Add",
    "Api": "add",
    "Parameters": {
      "num1": "${var.mynum1}",
      "num2": 20
    },
    "Expected": {
      "result": 119
    }
  }
]
```
## Using Functions
```json
[
  {
    "Name": "Math.Add",
    "Api": "add",
    "Parameters": {
      "num1": "${random()}",
      "num2": 20
    }
  }
]
```

### Chaining outputs to inputs
```json
[
  {
    "Name": "ExtractVariable Test",
    "Api": "EchoString",
    "Parameters": {
      "input" :  "echo this one." 
    }
  },
  {
    "Name": "UsingVariable From previous test",
    "Api": "EchoString",
    "Parameters": {
      "input": "${extract(key=result)}"
    },
    "Expected": {
      "result": "echo this one."
    }
  }
]
```

